
using System.Linq;

public partial class PlayerManager
{
    // 키보드에서 인덱스를 입력받아 해당 인덱스에 총을 반환.
    public Gun SelectGun(int index)
    {
        return _Guns[index - 1];
    }

    // 현재 들고 있는 총을 반환한다.
    public Gun GetCurrentGun()
    {
        // IsInvoking이 어떻게 해서 작동하는 지 잘 모르겠음.
        // 현재 Instance화 된 총이 IsInvoking을 만족하는 것인지 아닌지 다른 의미가 있는지 모르겠음.
        // gun 중에서 gun.name을 가진 객체를 찾아서 반환함.
        _Guns.Where(gun => gun.IsInvoking()).Select(gun => gun.name);
        return _Guns.First();
    }
}
