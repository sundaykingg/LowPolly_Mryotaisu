using Assets.Low_Poly_FPS_Pack.Components.Scripts.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG_05_Fire : MonoBehaviour
{
    [SerializeField] GameObject m_bulletPrefab;

    BulletPool m_bullePool;
    [SerializeField] GameObject m_fir_point;

    [SerializeField] float m_lifTime;

    [SerializeField] float trajectoryCorrectionDistance = 5.0f;

    Rigidbody m_bullet_rb;

    [SerializeField] float m_bulletForce;

    [SerializeField] Transform firstCamera;

    [SerializeField] Transform thirdCamera;

    Transform characterCamera;

    [SerializeField] bool isFpp;

    [Tooltip("The audio clip that is played while fire."), SerializeField]
    private AudioClip fireSound;

    private AudioSource _audioSource;

    private List<BulletCorrection> trajectoryCorrectionList;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("11");
        m_bullePool = GameObjectPoolManager.instance.CreatGameObjectPool<BulletPool>("SMG_05_Bullet");
        m_bullePool.prefab = m_bulletPrefab;
        trajectoryCorrectionList = new List<BulletCorrection>();

        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = fireSound;
        _audioSource.loop = false;

        firstCamera.gameObject.SetActive(isFpp);
        thirdCamera.gameObject.SetActive(!isFpp);
    }

    private void PlayFireSounds()
    {
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 posi = m_fir_point.GetComponent<Transform>().position;
            Quaternion rota = m_fir_point.GetComponent<Transform>().rotation;
            GameObject m_bullet = m_bullePool.Get(posi, m_lifTime, rota); //四元数设置方向基本一致
            m_bullet_rb = m_bullet.GetComponent<Rigidbody>();
            m_bullet_rb.velocity = m_bullet.transform.up * m_bulletForce;
            PlayFireSounds();
            GectCorrectionVector(m_bullet, posi);

            //GameObject m_bullet = m_bullePoolt.Get(posi, 3.0f);
            // m_bullet.transform.Rotate(new Vector3(0,0,90));  旋转设置方向每一次都会不一样
            //m_bullet_rb = m_bullet.GetComponent<Rigidbody>();
            //m_bullet_rb.AddForce(m_bullet_force, ForceMode.VelocityChange);
            //GameObjectPoolManager.instance.GetGameObject("SpherePool", new Vector3(x, y, 0), 1);
        }
        TrajectoryCorrection();

        if (Input.GetKeyDown(KeyCode.V)) {
            ChangeView();
        }
    }

    private void GectCorrectionVector(GameObject m_bullet, Vector3 lastPosition) {
        if (isFpp)
        {
           characterCamera = firstCamera;
        }
        else {
            characterCamera = thirdCamera;
        }
        Vector3 camera2Muzzle = m_fir_point.transform.position - characterCamera.position;
        Vector3 _correctionVector = Vector3.ProjectOnPlane(-camera2Muzzle, characterCamera.forward);
        Debug.DrawRay(characterCamera.position, camera2Muzzle, Color.red, 10f);
        Debug.DrawRay(characterCamera.position, -camera2Muzzle, Color.green, 10f);
        Debug.DrawRay(characterCamera.position, characterCamera.forward, Color.blue, 10f);
        Debug.DrawRay(characterCamera.position, _correctionVector, Color.magenta, 10f);
        BulletCorrection bulletCorrection = new BulletCorrection(m_bullet, new Vector3(), false, _correctionVector,lastPosition);
        trajectoryCorrectionList.Add(bulletCorrection);
    }

    private void TrajectoryCorrection() {
        for (int i=0;i<trajectoryCorrectionList.Count;i++) {
            BulletCorrection bc = trajectoryCorrectionList[i];
            if (!bc.hastrajectoryCorrected && bc.consumedCorrectionVector.magnitude < bc._correctionVector.magnitude) {
                Vector3 correctionLeft = bc._correctionVector - bc.consumedCorrectionVector;
                float distanceThisFram = (bc.bullet.transform.position - bc.lastPosition).magnitude;
                Vector3 correctionThisFram = (distanceThisFram / trajectoryCorrectionDistance) * bc._correctionVector;
                correctionThisFram = Vector3.ClampMagnitude(correctionThisFram,correctionLeft.magnitude);
                bc.consumedCorrectionVector += correctionThisFram;

                bc.bullet.transform.position += correctionThisFram;
                if (Mathf.Abs(bc.consumedCorrectionVector.sqrMagnitude - bc._correctionVector.sqrMagnitude) < Mathf.Epsilon) {
                    bc.hastrajectoryCorrected = true;
                    trajectoryCorrectionList.Remove(bc);
                    continue;
                }
                Debug.DrawRay(bc.bullet.transform.position,bc.bullet.transform.up,Color.red,10f);
            }
            
        }
    }

    public class BulletCorrection{
        public GameObject bullet;
        public Vector3 consumedCorrectionVector;
        public bool hastrajectoryCorrected;
        public Vector3 _correctionVector;
        public Vector3 lastPosition;

        public BulletCorrection(GameObject bullet, Vector3 consumedCorrectionVector, bool hastrajectoryCorrected, Vector3 _correctionVector, Vector3 lastPosition) {
            this.bullet = bullet;
            this.consumedCorrectionVector = consumedCorrectionVector;
            this.hastrajectoryCorrected = hastrajectoryCorrected;
            this._correctionVector = _correctionVector;
            this.lastPosition = lastPosition;
        }
    }

    private void ChangeView()
    {
        isFpp = !isFpp;
        firstCamera.gameObject.SetActive(isFpp);
        thirdCamera.gameObject.SetActive(!isFpp);
    }

    private void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("对方是" + collision.gameObject.name);
    }
}
