using Models;

namespace Services{
    public interface IMarketService{
        void vender(Productos _product, int _quantity, Usuarios user, Mercado mercado);

    }

}