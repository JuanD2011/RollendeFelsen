using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EngineUnity : MonoBehaviour {

    [SerializeField] Text currencyUno, currencyDos, currencyTres;//currency
    [SerializeField] Text compras, descarte;
    [SerializeField] Text[] itemsTxt;
    [SerializeField] Text[] itemsInvTxt;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] GameObject canvasShop, canvasInventory;
    [SerializeField] GameObject initialText, readme;
    [SerializeField] InputField descarteInput;

    void Start () {
        audioClips = Resources.LoadAll<AudioClip>("Sounds");

        readme.SetActive(false);
        Shop.OnSatisfactoria += CompraTxtSatisfactoria;
        Shop.OnInsatisfactoria += CompraTxtInsatisfactoria;
        Shop.OnExisteNonConsumable += YaExisteNonConsumable;
        Inventario.OnDescarteSatisfactorio += DescarteSatisfactorio;
        Inventario.OnDescarteNonConsumable += NoPuedesDescartarEsto;
        Inventario.OnConsumirItem += ItemConsumido;
        Inventario.OnConsumirNonConsumable += ItemNoConsumido;
        canvasShop.SetActive(false);
        canvasInventory.SetActive(false);
        WriteCurrency();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            canvasShop.SetActive(true);
            canvasInventory.SetActive(false);
            initialText.SetActive(false);
            readme.SetActive(false);
            GetComponent<AudioSource>().clip = audioClips[1];
            GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            canvasInventory.SetActive(true);
            canvasShop.SetActive(false);
            initialText.SetActive(false);
            readme.SetActive(false);
            GetComponent<AudioSource>().clip = audioClips[1];
            GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canvasShop.activeInHierarchy || canvasInventory.activeInHierarchy || readme.activeInHierarchy)
            {
                readme.SetActive(false);
                canvasShop.SetActive(false);
                canvasInventory.SetActive(false);
                initialText.SetActive(true);
                GetComponent<AudioSource>().clip = audioClips[0];
                GetComponent<AudioSource>().Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            if (canvasShop.activeInHierarchy || canvasInventory.activeInHierarchy || initialText.activeInHierarchy)
            {
                readme.SetActive(true);
                canvasShop.SetActive(false);
                canvasInventory.SetActive(false);
                initialText.SetActive(false);
            }
        }
    }

    private void WriteCurrency() {
        currencyUno.text = Inventario.Instancia.Billetera[TypeCurrency.firstCurrency].ToString("0");
        currencyDos.text = Inventario.Instancia.Billetera[TypeCurrency.secondCurrency].ToString("0");
        currencyTres.text = Inventario.Instancia.Billetera[TypeCurrency.thirdCurrency].ToString("0");
    }

    private void CompraTxtSatisfactoria() {
        StartCoroutine(TxtCompra());
        WriteCurrency();
        GetComponent<AudioSource>().clip = audioClips[2];
        GetComponent<AudioSource>().Play();
    }


    private void CompraTxtInsatisfactoria() {
        StartCoroutine(TxtInsatisfactoria());
        WriteCurrency();
        GetComponent<AudioSource>().clip = audioClips[3];
        GetComponent<AudioSource>().Play();
    }


    private void YaExisteNonConsumable() {
        StartCoroutine(TxtYaExiste());
        GetComponent<AudioSource>().clip = audioClips[4];
        GetComponent<AudioSource>().Play();
    }

    private void DescarteSatisfactorio() {
        StartCoroutine(TxtDescarte());
        GetComponent<AudioSource>().clip = audioClips[3];
        GetComponent<AudioSource>().Play();
    }

    private void NoPuedesDescartarEsto() {
        StartCoroutine(TxtNoPuedesDescartarEsto());
        GetComponent<AudioSource>().clip = audioClips[3];
        GetComponent<AudioSource>().Play();
    }

    private void ItemConsumido() {
        StartCoroutine(TxtItemConsumido());
        GetComponent<AudioSource>().clip = audioClips[4];
        GetComponent<AudioSource>().Play();
    }

    private void ItemNoConsumido() {
        StartCoroutine(TxtItemNoConsumido());
        GetComponent<AudioSource>().clip = audioClips[3];
        GetComponent<AudioSource>().Play();
    }

    IEnumerator TxtCompra() {
        compras.text = "Compra hecha";
        yield return new WaitForSeconds(0.5f);
        compras.text = " ";
    }

    IEnumerator TxtInsatisfactoria() {
        compras.text = "No tienes disponibilidad monetaria";
        yield return new WaitForSeconds(0.5f);
        compras.text = " ";
    }

    IEnumerator TxtYaExiste()
    {
        compras.text = "Ya posees este ítem";
        yield return new WaitForSeconds(0.5f);
        compras.text = " ";
    }

    IEnumerator TxtDescarte()
    {
        descarte.text = "Ha sido descartado";
        yield return new WaitForSeconds(0.5f);
        descarte.text = " ";
    }

    IEnumerator TxtNoPuedesDescartarEsto() {
        descarte.text = "No lo puedes descartar";
        yield return new WaitForSeconds(0.5f);
        descarte.text = " ";
    }

    IEnumerator TxtItemConsumido() {
        descarte.text = "Item consumido";
        yield return new WaitForSeconds(0.5f);
        descarte.text = " ";

    }

    IEnumerator TxtItemNoConsumido()
    {
        descarte.text = "No puedes consumir item";
        yield return new WaitForSeconds(0.5f);
        descarte.text = " ";
    }

    public void Boton(string _ids)
    {
        char[] ids = _ids.ToCharArray();
        if (ids.Length == 1)
        {
            int _id = (int)char.GetNumericValue(ids[0]);
            ShopController._shopController.mShop.Comprar(_id);
            Item item = Inventario.Instancia.ConversorIdtoItem(_id);
            itemsTxt[_id - 1].text = Inventario.Instancia.PInventario[item].ToString();
            itemsInvTxt[_id - 1].text = Inventario.Instancia.PInventario[item].ToString();
        }
        else
        {
            foreach (char c in ids)
            {
                int _id = (int)char.GetNumericValue(c);
                ShopController._shopController.mShop.Comprar(_id);
                Item item = Inventario.Instancia.ConversorIdtoItem(_id);
                itemsTxt[_id - 1].text = Inventario.Instancia.PInventario[item].ToString();
                itemsInvTxt[_id - 1].text = Inventario.Instancia.PInventario[item].ToString();
            } 
        }
    }

    public void BotonDescarte(int _id) {
        int textoInput;
        if (descarteInput.text != "") {
            textoInput = int.Parse(descarteInput.text);
            Item item = Inventario.Instancia.ConversorIdtoItem(_id);
            if (item != null)
            {
                Inventario.Instancia.DescartarItem(_id, textoInput);
                itemsInvTxt[_id-1].text = Inventario.Instancia.PInventario[item].ToString();
            }
        }
    }

    public void BotonConsumir(int _id) {
        Item item = Inventario.Instancia.ConversorIdtoItem(_id);
        if (item != null)
        {
            Inventario.Instancia.ConsumirItem(item);
            itemsInvTxt[_id - 1].text = Inventario.Instancia.PInventario[item].ToString(); 
        }
    }

    #region modificarCurrency
    public void ModificarCurrencyUno(string _input) {
        int cantidad = int.Parse(_input);
        Inventario.Instancia.Billetera[TypeCurrency.firstCurrency] = cantidad;
        WriteCurrency();
    }

    public void ModificarCurrencyDos(string _input)
    {
        int cantidad = int.Parse(_input);
        Inventario.Instancia.Billetera[TypeCurrency.secondCurrency] = cantidad;
        WriteCurrency();
    }

    public void ModificarCurrencyTres(string _input)
    {
        int cantidad = int.Parse(_input);
        Inventario.Instancia.Billetera[TypeCurrency.thirdCurrency] = cantidad;
        WriteCurrency();
    }
    #endregion

}
