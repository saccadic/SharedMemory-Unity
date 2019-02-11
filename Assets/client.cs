using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;

public class client : MonoBehaviour
{
    public MemoryMappedFile share_mem;
    public MemoryMappedViewAccessor accessor;

    ~client()
    {
        accessor.Dispose();
        share_mem.Dispose();
    }

    // Start is called before the first frame update
    void Start()
    {
        share_mem = MemoryMappedFile.OpenExisting("shared_memory");
        accessor = share_mem.CreateViewAccessor();
    }

    // Update is called once per frame
    void Update()
    {
        int size = accessor.ReadInt32(0);
        char[] data = new char[size];
        accessor.ReadArray<char>(sizeof(int), data, 0, data.Length);
        string str = new string(data);
        Debug.Log("Data = " + str);
    }
}
