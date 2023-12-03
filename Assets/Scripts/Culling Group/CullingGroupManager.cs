using UnityEditor;
using UnityEngine;


/// <summary>
/// Objelerin dinamik olarak yüklenmesinden ve kaldırılmasından sorumlu olan class.
/// </summary>
public class CullingGroupManager : MonoBehaviour
{

    [SerializeField] private Transform _player;

    /// <summary>
    /// Objelerin max ne kadar alan içerisinde renderlanacak
    /// </summary>
    [SerializeField] private float _renderDistance;

    
    private CullingGroup _group;
    private BoundingSphere[] _spheres;

    /// <summary>
    /// Bazı objelerin yakınımdayken mutla aktif olmasını istediğimiz değer.
    /// Exmple: köprünün üstündeyken köprüye bakmadığımızda kapanırsa
    /// köprüden düşüyoruz.
    /// </summary>
    private float _mustRenderDistance=1f;
    private Transform[] _groupItems;

    /// <summary>
    /// İlgili Arraylerin Initalize edilmesi
    /// </summary>
    private void Awake() 
    {
        _group = new CullingGroup();
        _groupItems = new Transform[transform.childCount];
        _spheres = new BoundingSphere[transform.childCount];
    }


    private void Start() 
    {
        FillGroupItems();
        SetSphereDatas();
        InitializeCullingGroup();
    }

    private void Update() 
    {
        _group.SetDistanceReferencePoint(_player);
    }

    private void OnDestroy() 
    {
        _group.Dispose();
        _group=null;
    }

    /// <summary>
    /// scriptin bağlı olduğu transformun bütün çocuklarının dynamic load işlemine dahil olacağı bir senaryo kurdum.
    /// ondan dolayı scriptin bütün çoklarına erişme şekli.
    /// </summary>
    private void FillGroupItems()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _groupItems[i]=transform.GetChild(i);
        }

    }

    /// <summary>
    /// Culling Group işlemi için gereken BoundingSphere değerlerinin atanması.
    /// İçerisinde kendi oluşturduğum static classtan mesh dataları ile işlem yaptırıp 
    /// meshin centerını ve yarıçap ataması için gerekli olan değeri alıyorum.
    /// </summary> 
    private void SetSphereDatas()
    {
        for(int i =0;i < _groupItems.Length;i++)
        {
            _spheres[i].position= MeshDataGetter.GetMeshCenter(_groupItems[i]);
            _spheres[i].radius= MeshDataGetter.GetItemRadius(_groupItems[i]);
        }
    }

    /// <summary>
    /// Culling Group değerlerinin Initialize edilmesi
    /// and GroupStateChanged subscribed to OnStateChanged delegate
    /// </summary>
    private void InitializeCullingGroup()
    {
        _group.SetBoundingSpheres(_spheres);
        _group.SetBoundingDistances(new float[]{0,_mustRenderDistance,_renderDistance});
        _group.targetCamera = Camera.main;
        _group.onStateChanged += GroupStateChanged;
    }


    /// <summary>
    /// onStateChanged tetiklendiğinde çalışıyor.
    /// obje görünür olduysa veya uzaklığı 1. birimdeyse eventIndexinden gameobject açılıyor.
    /// obje görünmez hale geçtiyse veya uzaklığı 3. bölgede ise gameobject kapanıyor.
    /// GameObject üzerinden işlem yaptım çünkü dökümanda Object olarak belirtilmişti.
    /// Kendi fikrim sadece Mesh ve animasyonunu kapatmak olurdu.
    /// </summary>
    /// <param name="pEvent"></param>
    private void GroupStateChanged(CullingGroupEvent pEvent)
    {
        if(pEvent.hasBecomeVisible||pEvent.currentDistance==1)
            _groupItems[pEvent.index].gameObject.SetActive(true);
        else if(pEvent.hasBecomeInvisible||pEvent.currentDistance==3)
            _groupItems[pEvent.index].gameObject.SetActive(false);
    }



    /// <summary>
    ///  Her bir objenin BoundingSphere datasını görmek için kullandım.
    /// </summary>
    private void OnDrawGizmosSelected() 
    {
        if(!EditorApplication.isPlaying) return;
        Gizmos.color = Color.red;
        for (int i = 0; i < _spheres.Length; i++)
        {
            Gizmos.DrawWireSphere(_spheres[i].position,_spheres[i].radius);
        }
    }
}
