public class ShopController{

    public Shop mShop;
    private static ShopController shopController;
    public static ShopController _shopController
    {
        get
        {
            if (shopController == null)
            {
                shopController = new ShopController();
            }
            return shopController;
        }
    }

    public ShopController()
    {
        mShop = new Shop();
    }
}
