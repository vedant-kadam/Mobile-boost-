using UnityEngine.SceneManagement;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    

    Rigidbody rigidbody;
    AudioSource audioSource;
    public float UpWardForce = 1f;
    public float SideWaysForce = 1f;
    public float DelayTimeBetweenScean = 1.5f;
    [SerializeField] AudioClip[] MainEngine;
public ParticleSystem[] MainEnginspartical;
    enum State { Alive,Dying, Transding }
    State state = State.Alive;
    public bool okthrust= false;
    private void Start()
    {
       
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
 void FixedUpdate()
    {
        if(state ==State.Alive)       
        {
            Thrusts();
            rotateit();
        }
        
    }
   public void ThrustButton()
    {
        Thrusts();
        //rotateit();
    }
    

    void Thrusts()
    {
        if(Input.GetKey(KeyCode.Space)||okthrust)
        {

            OnInputThrusting();
            
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
            MainEnginspartical[0].Stop();
        }



       
                
    }
    public void Oktr()
    {
        okthrust = true;
    }
    public void NonINputThrust()
    {
        okthrust = false;
       // if (audioSource.isPlaying)
         //   audioSource.Stop();
        //MainEnginspartical[0].Stop();
    }
    public void OnInputThrusting()
    {

        rigidbody.AddRelativeForce(Vector3.up * UpWardForce * Time.deltaTime);
        print("Thrust");
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(MainEngine[0]);
            MainEnginspartical[0].Play();

        }
    }
    public bool rori=false, rolf=false;
    void rotateit()
    {
        rigidbody.angularVelocity = Vector3.zero;
      //  rigidbody.freezeRotation = true;//here we freeze the rotation it does on its own so that it does no t rotate on itown while we are thrusting
        if (Input.GetKey(KeyCode.A)||rolf)
        {
            print(" Left");
            transform.Rotate(Vector3.forward * SideWaysForce * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D)||rori)
        {
            print(" right");
            transform.Rotate(-Vector3.forward * SideWaysForce * Time.deltaTime);
        }
      //  rigidbody.freezeRotation = false;//we regain the normal behaviour
    }
    public void Rotateright()
    {
        rori = true;
       // rigidbody.angularVelocity = Vector3.zero;
        //print(" right");
        //transform.Rotate(-Vector3.forward * SideWaysForce * Time.deltaTime);
    }
    public void rotateLeft()
    {
        rolf = true;
        //rigidbody.angularVelocity = Vector3.zero;
        //print(" Left");
        //transform.Rotate(Vector3.forward * SideWaysForce * Time.deltaTime);

    }
    public void NotRotateLeft()
    {
        rolf = false;
    }
    public void NotRotateRight()
    {
        rori = false;
    }
    public void CurrentRotation()
    {
       //rigidbody.angularVelocity = Vector3.zero;
        //transform.rotation = transform.rotation;
    }
   void OnCollisionEnter(Collision collision)
    {

        if (state != State.Alive)
        {
            return;
        }
      
        switch (collision.gameObject.tag)
        {
            case "safe": print("You are safe");

                state = State.Alive;
                break;
            case "Finish": SUccessSequence();
                break;
            case "Enemys": FalireSequence();
                break;
                    

        }
    }
    void SUccessSequence()
    {
        print("You have cleared a level");
       // FindObjectOfType<Follower>().OkCollidedd = true;
        state = State.Transding;
        audioSource.Stop();

        audioSource.PlayOneShot(MainEngine[2]);
        MainEnginspartical[1].Play();
        FindObjectOfType<Follower>().OkCollidedd = true;
        Invoke("LoadTheNextScean", DelayTimeBetweenScean);
    }
    void FalireSequence()
    {
        print("You are dead man");
     //  
        audioSource.Stop();
        audioSource.PlayOneShot(MainEngine[1]);
        state = State.Dying;

        MainEnginspartical[2].Play();
        FindObjectOfType<Follower>().OkCollidedd = true;
        Invoke("LeadTheCurrentScean", DelayTimeBetweenScean);
    }
    void LoadTheNextScean()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void LeadTheCurrentScean()
    {
        SceneManager.LoadScene(0);

    }
}
