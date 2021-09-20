
using System.Linq;

public partial class PlayerManager
{
    public void SelectGun(int index)
    {
        
    }

    public Gun GetCurrentGun()
    {
        _Guns.Where(gun => gun.IsInvoking()).Select(gun => gun.name);
        return _Guns.First();
    }
}
