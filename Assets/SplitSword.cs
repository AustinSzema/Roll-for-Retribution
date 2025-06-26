using System;
using UnityEngine;

public class SplitSword : Weapon
{
    [SerializeField] private Rigidbody leftSplit;
    [SerializeField] private Rigidbody rightSplit;

    public override void Snap()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);

        if (leftSplit)
        {
            leftSplit.gameObject.SetActive(true);
            leftSplit.gameObject.transform.position = transform.position;
            leftSplit.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            leftSplit.linearVelocity = rb.linearVelocity + Vector3.left * 50f;
            leftSplit.AddForce(Vector3.down * slamForce);
        }

        if (rightSplit)
        {
            rightSplit.gameObject.SetActive(true);
            rightSplit.gameObject.transform.position = transform.position;
            rightSplit.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            rightSplit.linearVelocity = rb.linearVelocity + Vector3.right * 50f;
            rightSplit.AddForce(Vector3.down * slamForce);
        }
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(Vector3.down * slamForce);
        
    }

    public override void Attract(Vector3 magnetPosition)
    {
        base.Attract(magnetPosition);
        if (leftSplit)
        {
            leftSplit.gameObject.SetActive(false);
        }

        if (rightSplit)
        {
            rightSplit.gameObject.SetActive(false);
        }
        
    }

    private void Update()
    {
        float scale = Vector3.Distance(transform.position, GameManager.Instance.handPosition);
        float clampedScale = Mathf.Clamp(scale, 1f, 3f);
        transform.localScale = new Vector3(clampedScale, clampedScale, clampedScale);
        rb.AddRelativeTorque(Vector3.right * 100f, ForceMode.Impulse);

    }
}
