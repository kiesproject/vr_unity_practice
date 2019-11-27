using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBullet : MonoBehaviour
{
    public GameObject bulletE;
    public int damege = 5;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            var o = Instantiate(bulletE, contact.point, Quaternion.identity);
            o.transform.LookAt(contact.point + contact.normal);
            var ib = collision.collider.gameObject.GetComponent<IButtle>();
            if (ib != null)
            {
                ib.AddDamage(damege, this.gameObject);
            }
            Destroy(gameObject);
        }
    }

}
