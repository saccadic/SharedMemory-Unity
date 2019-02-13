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

    private MemoryMappedFile share_mem_image;
    private MemoryMappedViewAccessor accessor_image;

    public Text text;
    public RawImage RImg;

    public int size;
    public string sharedMsg;
    public int reciveMesSize;

    private byte[] data;
    private byte[] data_image;

    public Texture2D tex;

    ~client()
    {
        accessor.Dispose();
        share_mem.Dispose();

        accessor_image.Dispose();
        share_mem_image.Dispose();
    }

    // Start is called before the first frame update
    void Start()
    {
        share_mem = MemoryMappedFile.OpenExisting("0");
        accessor = share_mem.CreateViewAccessor();
        size = 1024;
        data = new byte[size];

        share_mem_image = MemoryMappedFile.OpenExisting("1");
        accessor_image = share_mem_image.CreateViewAccessor();
        data_image = new byte[640 * 480 * 3];

        tex = new Texture2D(640, 480, TextureFormat.RGB24, false);
    }

    // Update is called once per frame
    void Update()
    {
        double num = accessor.ReadDouble(0);
        reciveMesSize = accessor.ReadArray<byte>(sizeof(double), data, 0, size);
        sharedMsg = System.Text.Encoding.UTF8.GetString(data);

        int imageSize = accessor_image.ReadArray<byte>(0, data_image, 0, 640 * 480 * 3);
        tex.LoadRawTextureData(data_image);
        tex.Apply();
        

        RImg.texture = tex;

        text.text = sharedMsg;
    }
}
