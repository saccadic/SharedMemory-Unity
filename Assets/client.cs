using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Text;
using UnityEngine;

public class client : MonoBehaviour
{
    public MemoryMappedFile share_mem;
    public MemoryMappedViewAccessor accessor;
    public int size;
    public char[] data;

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

        size = 10;
        data = new char[size];
    }

    // Update is called once per frame
    void Update()
    {
        accessor.ReadArray<char>(sizeof(int), data, 0, data.Length);

        //char data = accessor.ReadChar(0);
        string str = new string(data);
        //string str2 = Encoding.GetEncoding("shift-jis").GetString(str);
        Debug.Log(str);
    }
}
