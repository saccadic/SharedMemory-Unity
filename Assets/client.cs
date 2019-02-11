using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class client : MonoBehaviour
{
    private MemoryMappedFile share_mem;
    private MemoryMappedViewAccessor accessor;

    public Text text;

    public int size;
    public string sharedMsg;
    public int reciveMesSize;
    public byte[] data;


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
        size = 1024;
        data = new byte[size];
    }

    // Update is called once per frame
    void Update()
    {
        double num = accessor.ReadDouble(0);
        reciveMesSize = accessor.ReadArray<byte>(sizeof(double), data, 0, size);
        sharedMsg = System.Text.Encoding.UTF8.GetString(data);

        text.text = sharedMsg;
    }
}
