using System;
using System.Runtime.InteropServices;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SharedMemory : MonoBehaviour
{

    
    [DllImport ("SharedMemoryForUnity")]
    private static extern  void setupSharedMemory(string name, int size);
    
   [DllImport ("SharedMemoryForUnity")]
   private static extern IntPtr getMsg();

    private IntPtr msgPtr = IntPtr.Zero;

    public byte[] memory;

    public string sharedMsg;

    // Start is called before the first frame update
    void Start()
    {
        setupSharedMemory("0",1024);
        memory = new byte[1024];
    }

    // Update is called once per frame
    void Update()
    {
        msgPtr = getMsg();
        
        Marshal.Copy(msgPtr, memory, 0, 1024);
        
        sharedMsg = System.Text.Encoding.UTF8.GetString(memory);
    }
}
