using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HieuUng_Matmau : MonoBehaviour
{
    public GameObject particlePrefab;
    // âm thanh
    public AudioClip file_am_thanh;
    private AudioSource audio_src;
    // Start is called before the first frame update
    void Start()
    {
        audio_src = GetComponent<AudioSource>();
        audio_src.clip = file_am_thanh;
    }

    // Update is called once per frame
    void Update()
    {
   
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="trai")
        {
            Debug.Log("Đã Va Chạm Vào Nhạc");
            audio_src.Play();
            Invoke("AudioStop", 0.5f);
            ShowParticles();
        }
    }


    private void AudioStop()
    {
        audio_src.Stop();
    }
    
    
    void ShowParticles()
    {
        // Tạo một đối tượng hạt từ prefab
        GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();
        particleSystem.Play();
  
        Debug.Log(particle.transform.position.ToString());
  
        // Hủy bỏ hạt sau một khoảng thời gian ngẫu nhiên
        // Destroy(particle, Random.Range(5.0f, 7.0f));
    }
}
