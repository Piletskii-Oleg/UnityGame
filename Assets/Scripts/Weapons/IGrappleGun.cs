using System.Collections;

namespace Weapons
{
    public interface IGrappleGun
    {
        IEnumerator StartGrapple();
        
        IEnumerator StopGrapple();
        
        IEnumerator ExecuteGrapple();
    }
}