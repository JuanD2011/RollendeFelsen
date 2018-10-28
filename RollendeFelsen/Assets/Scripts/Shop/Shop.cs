using System.Collections.Generic;

public class Shop
{
    private Item itemUno;
    private Item itemDos;
    private Item itemTres;
    
    public delegate void Compra(Item _item);
    public static Compra delCompra;

    public Shop()
    {
        itemUno = new NonCosumable(1,2,0,0);
        itemDos = new NonCosumable(2,4,0,0);
        itemTres = new NonCosumable(3, 10, 0, 0);
    }

    public void Comprar(int _id)
    {
        Item _item = null;

        switch (_id)
        {
            case 1:
                _item = itemUno;
                break;
            case 2:
                _item = itemDos;
                break;
            case 3:
                _item = itemTres;
                break;
            default:
                break;
        }

        if (Inventario.Instancia.VerificaDisponibilidadMonetaria(_item.Costo))
        {
            if (!Inventario.Instancia.VerificarExistencia(_item))
            {
                //Compra hecha
                Inventario.Instancia.Adquisicion(_item);
                Dictionary<TypeCurrency, int> cost = new Dictionary<TypeCurrency, int>();
                cost.Add(TypeCurrency.firstCurrency, _item.Costo[TypeCurrency.firstCurrency]*-1);
                Inventario.Instancia.ActualizarCurrency(cost);
                delCompra(_item);
            }
        }
    }
}
