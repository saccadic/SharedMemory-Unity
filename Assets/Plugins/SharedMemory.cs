using System;
using System.Runtime.InteropServices;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharedMemory : MonoBehaviour
{

    
    [DllImport ("SharedMemoryForUnity")]
    private static extern  void setupSharedMemory(string name, int size);
    
    [DllImport ("SharedMemoryForUnity")]
    private static extern  void setupSharedMemory_img(string name, int size);
    
   [DllImport ("SharedMemoryForUnity")]
   private static extern IntPtr getMsg();

   [DllImport ("SharedMemoryForUnity")]
   private static extern IntPtr getImg();

    private IntPtr msgPtr = IntPtr.Zero;
    private IntPtr imgPtr = IntPtr.Zero;

    public byte[] memory;
    public byte[] memory_img;

    public string sharedMsg;

    public int size;
    public int size_img;

    public Texture2D tex;
    
    public RawImage RImg;
    public Text text;
    public Text text_fps;
    
    void Start()
    {
        size = 1024;
        size_img = 640*480*3;
        
        setupSharedMemory("0",size);
        setupSharedMemory_img("1",size_img);
        
        memory = new byte[size];
        memory_img = new byte[size_img];
        
        tex = new Texture2D(640, 480, TextureFormat.RGB24, false);
    }

    void Update()
    {
        float fps = 1f / Time.deltaTime;
        
        msgPtr = getMsg();
        imgPtr = getImg();
        
        Marshal.Copy(msgPtr, memory, 0, size);
        Marshal.Copy(imgPtr, memory_img, 0, size_img);
        
        sharedMsg = System.Text.Encoding.UTF8.GetString(memory);
        text.text = sharedMsg;
        
        tex.LoadRawTextureData(memory_img);
        tex.Apply();
        RImg.texture = tex;
         
        text_fps.text = fps.ToString();
    }
}
