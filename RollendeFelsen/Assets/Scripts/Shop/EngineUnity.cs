using UnityEngine;
using UnityEngine.UI;

public class EngineUnity : MonoBehaviour {

    [SerializeField] Text currencyUno;
    [SerializeField] Button[] buttonsShop;
    [SerializeField] Toggle[] skinSelected;

    public static bool isFirstSkin, isSecondSkin, isThirdSkin;

    void Start () {
        WriteCurrency();
        Shop.delCompra += Compra;
        
        InitToggles();
    }

    private void WriteCurrency() {
        currencyUno.text = Inventario.Instancia.Billetera[TypeCurrency.firstCurrency].ToString("0");
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
        skinSelected[_item.Id - 1].interactable = true;
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
    }

    private void DeselectSkin(int _index) {

        for (int i = 0; i < skinSelected.Length; i++) {
            if (i != _index - 1) {

            }
        }
    }
    private void InitToggles() {
        isFirstSkin = false;
        isSecondSkin = false;
        isThirdSkin = false;

        foreach (Toggle a in skinSelected) {
            a.interactable = false;
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

    public void ModificarCurrencyUno(int _amount) {
        Inventario.Instancia.Billetera[TypeCurrency.firstCurrency] += _amount;
        WriteCurrency();
    }

}
