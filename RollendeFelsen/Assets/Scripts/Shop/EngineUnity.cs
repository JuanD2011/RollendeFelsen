using UnityEngine;
using UnityEngine.UI;

public class EngineUnity : MonoBehaviour {

    [SerializeField] Text currencyUno;
    [SerializeField] Button[] buttonsShop;

    public static bool isFirstSkin, isSecondSkin, isThirdSkin;

    void Start () {
        LoadCurrency();
        Shop.delCompra += Compra;
        InitButtons();
        WriteCurrency();
    }

    private void WriteCurrency() {
        currencyUno.text = Inventario.Instancia.Billetera[TypeCurrency.firstCurrency].ToString();
    }

    private void InitButtons() {
        for (int i = 0; i < buttonsShop.Length; i++) {
            if (Inventario.Instancia.PInventario.ContainsValue(i + 1))
            {
                buttonsShop[i].interactable = false;
                buttonsShop[i].transform.GetChild(1).GetComponent<Button>().interactable = true;
            }
            else {
                buttonsShop[i].interactable = true;
                buttonsShop[i].transform.GetChild(1).GetComponent<Button>().interactable = false;
            }
            buttonsShop[i].transform.GetChild(2).GetComponent<Image>().enabled = false;
        }
    }

    private void Compra(Item _item) {
        switch (_item.Id)
        {
            case 1:
                buttonsShop[_item.Id - 1].interactable = false;
                break;
            case 2:
                buttonsShop[_item.Id - 1].interactable = false;
                break;
            case 3:
                buttonsShop[_item.Id - 1].interactable = false;
                break;
            default:
                break;
        }
        buttonsShop[_item.Id - 1].transform.GetChild(1).GetComponent<Button>().interactable = true;
        WriteCurrency();
    }

    public void SelectSkin(int _indexSkin) {
        switch (_indexSkin)
        {
            case 1:
                isFirstSkin = true;
                isSecondSkin = false;
                isThirdSkin = false;
                break;
            case 2:
                isSecondSkin = true;
                isFirstSkin = false;
                isThirdSkin = false;
                break;
            case 3:
                isThirdSkin = true;
                isFirstSkin = false;
                isSecondSkin = false;
                break;
            default:
                break;
        }

        for (int i = 0; i < buttonsShop.Length; i++) {
            if (i == _indexSkin - 1)
            {
                buttonsShop[_indexSkin - 1].transform.GetChild(2).GetComponent<Image>().enabled = true;
            }
            else if (i != _indexSkin-1) {
                buttonsShop[i].transform.GetChild(2).GetComponent<Image>().enabled = false;
            }
        }
    }

    public void Buy(string _ids)
    {
        char[] ids = _ids.ToCharArray();
        if (ids.Length == 1)
        {
            int _id = (int)char.GetNumericValue(ids[0]);
            ShopController._shopController.mShop.Comprar(_id);
            Item item = Inventario.Instancia.ConversorIdtoItem(_id);
        }
        else
        {
            foreach (char c in ids)
            {
                int _id = (int)char.GetNumericValue(c);
                ShopController._shopController.mShop.Comprar(_id);
                Item item = Inventario.Instancia.ConversorIdtoItem(_id);
            } 
        }
    }

    public void BotonConsumir(int _id) {
        Item item = Inventario.Instancia.ConversorIdtoItem(_id);
        if (item != null)
        {
            Inventario.Instancia.ConsumirItem(item);
        }
    }

    public void LoadCurrency() {
        int value = PlayerPrefs.GetInt("Coins");
        Inventario.Instancia.Billetera[TypeCurrency.firstCurrency] = value;
        WriteCurrency();
    }

    public void GiveCurrency(int _amount) {
        PlayerPrefs.SetInt("Coins", Inventario.Instancia.Billetera[TypeCurrency.firstCurrency] + _amount);
        WriteCurrency();
    }

}
