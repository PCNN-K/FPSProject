
using System.Linq;

public partial class PlayerManager
{
    public int selectedIndex = 0;
    // 키보드에서 인덱스를 입력받아 해당 인덱스에 총을 반환.
    public Gun SelectGun(int index)
    {
        return _Guns[index];
    }

    // 현재 들고 있는 총을 반환한다.
    public Gun GetCurrentGun()
    {

        return SelectGun(selectedIndex);
    }
}
