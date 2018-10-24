using System.Collections.Generic;

public abstract class Item
{
    private int id;
    public int Id
    {
        get
        {
            return id;
        }
    }
    private Dictionary<TypeCurrency, int> costo = new Dictionary<TypeCurrency, int>();
    public Dictionary<TypeCurrency, int> Costo
    {
        get
        {
            return costo;
        }

        set
        {
            costo = value;
        }
    }


    public Item(int _id, int _costoCurrencyUno, int _costoCurrencyDos, int _costoCurrencyTres) {
        id = _id;
        Costo.Add(TypeCurrency.firstCurrency, _costoCurrencyUno);
        Costo.Add(TypeCurrency.secondCurrency, _costoCurrencyDos);
        Costo.Add(TypeCurrency.thirdCurrency, _costoCurrencyTres);
    }

}
