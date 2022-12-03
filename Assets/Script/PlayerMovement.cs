using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{

    private float speed = 10f;
    private float jumpingPower = 24f;
    private bool isFacingRight = true;
    private float horizontal;
    PhotonView view;
    public GameObject player;
    public GameObject playerCamera;
    public Animator anim;
    public SpriteRenderer sr;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform bulletPosition;
  

    private void Start()
    {
        view = GetComponent<PhotonView>();
        if (view.IsMine)
            playerCamera.SetActive(true);
        else
            playerCamera.SetActive(false);
    }
    void Update()
    {
        if (view.IsMine)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
            if (Input.GetKeyDown(KeyCode.A))
                view.RPC("FlipTrue", RpcTarget.AllBuffered);
            if (Input.GetKeyDown(KeyCode.D))
                view.RPC("FlipFalse", RpcTarget.AllBuffered);
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Fire();
        }
     
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Fire()
    {
        if (sr.flipX == true)
        {
            GameObject obj =PhotonNetwork.Instantiate(bulletPrefab.name, new Vector2(bulletPosition.transform.position.x, bulletPosition.transform.position.y), Quaternion.identity, 0);
            obj.GetComponent<PhotonView>().RPC("ChangeDir_left",RpcTarget.All)
;
        }

        if (sr.flipX == false)
        {
            GameObject obj = PhotonNetwork.Instantiate(bulletPrefab.name, new Vector2(bulletPosition.transform.position.x, bulletPosition.transform.position.y), Quaternion.identity, 0);
        }
           

        /*GameObject bullet = ObjectPool.instance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = bulletPosition.position;
            bullet.SetActive(true);
        }*/
    }
    [PunRPC]
    private void FlipTrue()
    {
        sr.flipX = true;
    }
    [PunRPC]
    private void FlipFalse()
    {
        sr.flipX = false;
    }
}