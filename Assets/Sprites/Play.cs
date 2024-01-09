using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

using UnityEngine.UI; // Để sử dụng SceneManager
public class Play : MonoBehaviour
{

    public string ten_ma_hinh;
    public InputField  inputTenNguoiChoi;
    private bool isCheckDN;
   // public GameObject thongBao; // Thêm một biến để lưu GameObject chứa thông báo
    
    
    
    // lưu tên nhân vật
    public void LuuTen()
    {
        // lấy tên thông tin từ ô input
        string tenNV = inputTenNguoiChoi.text;
        PlayerPrefs.SetString("tenNhanVat", tenNV);
        
        // // Kiểm tra xem nếu tên nhân vật không trống thì ẩn thông báo
        // if (!string.IsNullOrEmpty(tenNV))
        // {
        //     thongBao.SetActive(false);
        // }
    }


    private void Start()
    {
        inputTenNguoiChoi.text =PlayerPrefs.GetString("tenNhanVat", "");
    }

    private void Update()
    {
        LuuTen();
    }

    public void buttonPlay()
    {
        
        string tenNV = inputTenNguoiChoi.text;
        if (string.IsNullOrEmpty(tenNV))
        {
            // Hiển thị thông báo cho người dùng
            // Ví dụ: bạn có thể sử dụng Debug.Log() hoặc hiển thị một thông báo trên giao diện
            Debug.Log("Tên nhân vật không được để trống. Vui lòng nhập tên.");
        }
        else
        {
            // Nếu tên nhân vật không trống, bạn có thể chuyển màn hình
            SceneManager.LoadScene(ten_ma_hinh);
        }
     
    }

 
    
    public void QuayVeManDangNhap()
    {
        SceneManager.LoadScene("Play"); // Thay "TenMaHinhDangNhap" bằng tên thật của màn hình đăng nhập của bạn
    }

    public void chonLever()
    {
        SceneManager.LoadScene(2);
    }
}
