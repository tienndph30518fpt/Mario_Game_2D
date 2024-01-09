
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DiChuyen : MonoBehaviour
{
    
    public Animator anim;
    private Rigidbody2D rb;
    
    
    
    public float speed = 4.0f; // Tốc độ di chuyển của nhân vật
    public float jumpForce = 10.0f; // Lực nhảy
    private bool isFacingRight = true; // Biến để kiểm tra hướng của nhân vật
    
    public GameObject tien;
    private Dictionary<GameObject, bool> collidedWithVatCan = new Dictionary<GameObject, bool>();
    
    public GameObject panel , text;
    // public GameObject PSBrick;
    public TextMeshProUGUI diemText;
    private int tong = 0;

    
  
    public Slider playerHeartSlider; // Slider hiển thị thanh máu
    private float mauToiDa = 100f; // Máu tối đa của nhân vật
    
    // chuyen man choi;
    public float thoiGianChuyenMan = 2f;// thời gian chuyên màn là 2 giây 
    public string tenManChuyenDen;
    
    // tên Nhân Vật
    public Text PlayName;
    

    private bool isMoving = false; // Biến để kiểm tra trạng thái di chuyển
    
    
    // animation 

    private bool moveLeft;
    
    private bool moveRight;

    private float horiontalMove;
    public float speeed = 5;
    
    
    // hiển thị số lượng máu theo khoảng
    
    public Gradient Gradient;// hiển thị màu theo lượng máu
    public Image fillcoler;// backgruond hiển thị máu
    
    void tinhTong(int score)
    {
        tong += score;
        diemText.text = "Điểm: " + tong;
        PlayerPrefs.SetInt("diem", tong); // Lưu điểm số vào PlayerPrefs
    }
    void Start()
    {

        //

        moveLeft = false;
        moveRight = false;
        
        
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // Đọc điểm số từ PlayerPrefs
        tong = PlayerPrefs.GetInt("diem", 0);

        // Cập nhật TextMeshProUGUI với điểm số đã lưu
        diemText.text = "Điểm: " + tong;

        if (PlayerPrefs.HasKey("mauHienTai"))
        {
            mauToiDa = PlayerPrefs.GetFloat("mauHienTai");
            playerHeartSlider.value = mauToiDa;
        }
      
        
        
        // kiểm tra lưu tên nhân vật
        if (PlayerPrefs.HasKey("tenNhanVat"))
        {
            string savedTen = PlayerPrefs.GetString("tenNhanVat", "");
            PlayName.text = savedTen;
        }
        
        playerHeartSlider.interactable = false;
    }

    void Update()
    {
        
        fillcoler.color = Gradient.Evaluate(playerHeartSlider.normalizedValue);// cập nhật hiển thị máu theo màu
        // run();
        // Jump();
        // Dance();
        playerName();
        
        MovePlayer();

    }

    public void PointerDownLeft()
    {
        moveLeft = true;
        Flip(-1);
        anim.SetFloat("Run", 1);
        rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
     
    }
    public void PointerUpLeft()
    {
        moveLeft = false;
        anim.SetFloat("Run", 0);
    }
    
    
    public void PointerDownRight()
    {
        moveRight = true;
        Flip(1);
        anim.SetFloat("Run", 1);
        rb.velocity = new Vector2(1 * speed, rb.velocity.y);
    }
    public void PointerUpRight()
    {
        moveRight = false;
        anim.SetFloat("Run", 0);
    }
    public void PointerDownJump()
    {
        moveRight = true;
        anim.SetFloat("Run", 1);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    public void PointerUpJump()
    {
        moveRight = false;
        anim.SetFloat("Run", 0);
    }


    
    
    


    private void MovePlayer()
    {
        if (moveLeft)
        {
            horiontalMove = -speeed;
        }else if (moveRight)
        {
            horiontalMove = speeed;
        }
        else
        {
            horiontalMove = 0;
        }
    }



    void Flip(int direction)
    {
        // Đảo hướng của nhân vật bằng cách thay đổi scale theo trục X
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }
    
    

    
    // vị trí tên người Chơi
    void playerName()
    {
        Vector3 vector3 = transform.position;
        vector3.y = vector3.y + 1.9f;
        vector3.x = vector3.x + 2.3f;
        PlayName.transform.position = vector3;
    }

    
    void run()
    {
        float move = Input.GetAxis("Horizontal");
        if (move > 0 && !isFacingRight)
        {
            Flip();
        }else if (move < 0 && isFacingRight)
        {
            Flip();
        }

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetFloat("Run", 1);
        }else if (Input.GetKey(KeyCode.D))
        {
            anim.SetFloat("Run", 1);
           
        }
        else
        {
            anim.SetFloat("Run", 0);
        }

        

        rb.velocity = new Vector2(move * speed, rb.velocity.y);
    }
    
    
    
    
    //  public void Jump()
    // {
    //     // Nếu nhấn phím Space, áp dụng lực nhảy lên
    //     if (Input.GetKey(KeyCode.W) && Mathf.Abs(rb.velocity.y) < jumpForce)
    //     {
    //         anim.SetFloat("Jump", 1);
    //         Debug.Log("NHấn Trên");
    //         // Thêm mã để xử lý hành động khi giá trị "Jump" được đặt thành 1.0
    //         rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    //     }
    //     else
    //     {
    //         anim.SetFloat("Jump", 0);
    //     }
    // }


    
    
    void Dance()
    {
        // Nếu nhấn phím s, đặt giá trị "Dance" thành 1.0
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetFloat("Dance", 1.0f);
            // Thêm mã để xử lý hành động khi giá trị "Dance" được đặt thành 1.0
        }
        else
        {
            // Nếu không nhấn phím s, đặt giá trị "Dance" về 0.0
            anim.SetFloat("Dance", 0.0f);
        }
    }

    void Move(Vector2 direction)
    {// hướng đi và tốc độ của nhân vật
        transform.Translate(direction * speed * Time.deltaTime);
    }

    
    
    void Flip()
    {
        // Đảo hướng của nhân vật bằng cách thay đổi scale theo trục X
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    
        // Cập nhật biến kiểm tra hướng
        isFacingRight = !isFacingRight;
    }
    
    
    
    

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Tien")
        {
            Destroy(other.gameObject,0.5f);
      
            tinhTong(5);
        }

    
        if (other.gameObject.tag == "HoiCham")
        {
            Destroy(other.gameObject, 0.5f);
            // audio_src.Play();
            // Instantiate(PSBrick, other.gameObject.transform.position, other.gameObject.transform.localRotation);// lấy vị chí hiện tại
            tinhTong(8);
        }
        if (other.gameObject.tag == "tren")
        {
            // Nhảy lên trên đầu thì được cộng điểm 
            Destroy(other.gameObject,0.5f);
            tinhTong(15);
        }


        if (other.gameObject.tag=="trai")
        {
            mauToiDa -= 10;
            playerHeartSlider.value = mauToiDa;
            PlayerPrefs.SetFloat("mauHienTai", mauToiDa);
            Debug.Log("va chạm");
            if (mauToiDa<=0)
            {
                tong = 0;
                // Lưu điểm số vào PlayerPrefs
                PlayerPrefs.SetInt("diem", tong);
                // Cập nhật TextMeshProUGUI với điểm số đã đặt lại
                diemText.text = "Điểm: " + tong;
            
                PlayerPrefs.DeleteKey("mauHienTai"); // Lưu trạng thái máu hiện tại vào PlayerPrefs
                // playerHeartSlider.value = mauToiDa;
                
                // chạm vào trái thì sẽ chết
                Time.timeScale = 0; // dừng lại sence;
                panel.SetActive(true);// show panel
                text.SetActive(false);
                playerHeartSlider.gameObject.SetActive(false);
                PlayName.GameObject().SetActive(false);
            }

        }

        if (other.gameObject.tag == "khung")
        {
            mauToiDa -= 100;
            playerHeartSlider.value = mauToiDa;
            PlayerPrefs.SetFloat("mauHienTai", mauToiDa);
            
            if (mauToiDa<=0)
            {
                tong = 0;
                // Lưu điểm số vào PlayerPrefs
                PlayerPrefs.SetInt("diem", tong);
                // Cập nhật TextMeshProUGUI với điểm số đã đặt lại
                diemText.text = "Điểm: " + tong;
            
                PlayerPrefs.DeleteKey("mauHienTai"); // Lưu trạng thái máu hiện tại vào PlayerPrefs
                // playerHeartSlider.value = mauToiDa;
                
                // chạm vào trái thì sẽ chết
                Time.timeScale = 0; // dừng lại sence;
                panel.SetActive(true);// show panel
                text.SetActive(false);
                playerHeartSlider.gameObject.SetActive(false);
                PlayName.GameObject().SetActive(false);
            }
        }
        
        if (other.gameObject.tag=="nextman")
        {
            ModelSeect();
        }
        
        
            
    }




    public void ModelSeect()
    {
        StartCoroutine(LoadTrang());
    }
    
    // Hàm này đợi một khoảng thời gian rồi cho phép nhân vật bị damage lại
    IEnumerator LoadTrang()
    {
        yield return new WaitForSeconds(0.5f); // Thời gian đợi, bạn có thể điều chỉnh nó theo ý muốn
        SceneManager.LoadScene(tenManChuyenDen);
    }
    }
    
    



    // void ThaTien()
    // {
    //     if (tien != null)
    //     {
    //         
    //         Instantiate(tien, transform.position, Quaternion.identity);
    //        
    //
    //     }
    // }
   

