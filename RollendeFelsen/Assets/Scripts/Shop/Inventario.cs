using System.Collections.Generic;

public class Inventario
{
    private static Inventario instancia;
    private Dictionary<Item, int> inventario = new Dictionary<Item, int>();
    public Dictionary<Item, int> PInventario
    {
        get
        {
            return inventario;
        }
    }

    private Dictionary<TypeCurrency, int> billetera = new Dictionary<TypeCurrency, int>();
    public Dictionary<TypeCurrency, int> Billetera
    {
        get
        {
            return billetera;
        }
    }

    public Inventario()
    {
        billetera.Add(TypeCurrency.firstCurrency, 40);
    }

    public static Inventario Instancia
    {
        get
        {
            if (instancia == null)
            {
                instancia = new Inventario();
            }
            return instancia;
        }
    }

    public void Adquisicion(Item _item)
    {
        if (_item is Consumable)
        {
            if (inventario.ContainsKey(_item))
            {
                inventario[_item] += 1;
            }
            else {
                inventario.Add(_item, 1);
            }
        }
        if ((_item is NonCosumable))
        {
            inventario.Add(_item, 1);
        }
    }

    public bool VerificarExistencia(Item _item)
    {
        bool existe = false;

        if (_item is NonCosumable && inventario.ContainsKey(_item))
        {
            existe = true;
        }

        return existe;
    }

    /// <summary>
    /// sólo se llama cuando se puede comprar
    /// </summary>
    /// <param name="_costo"></param>
    public void ActualizarCurrency(Dictionary<TypeCurrency, int> _costo)
    {
        billetera[TypeCurrency.firstCurrency] += _costo[TypeCurrency.firstCurrency];
    }

    public bool VerificaDisponibilidadMonetaria(Dictionary<TypeCurrency, int> _costo)
    {
        bool puedoComprarSignoPregunta = true;

        if (billetera[TypeCurrency.firstCurrency] < _costo[TypeCurrency.firstCurrency])
        {
            puedoComprarSignoPregunta = false;
        }
        return puedoComprarSignoPregunta;
    }

    public void DescartarItem(int _id, int _valorADescartar)
    {
        Item _item = ConversorIdtoItem(_id);

        if (_item is Consumable && _item != null && PInventario[_item] > 0)
        {
            inventario[_item] -= _valorADescartar;
            if (inventario[_item] < 0)
            {
                inventario[_item] = 0;
            }
        }
    }

    public void ConsumirItem(Item _item)
    {
        if (_item is Consumable)
        {
            if (PInventario[_item] > 0)
            {
                inventario[_item] -= 1;
            }
        }
    }

    public Item ConversorIdtoItem(int _id)
    {
        Item _item = null;

        foreach (KeyValuePair<Item, int> i in inventario)
        {
            if (_id == i.Key.Id)
            {
                _item = i.Key;
            }
        }

        return _item;
    }
}
