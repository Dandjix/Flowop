using UnityEngine;

public class VisqueuxMouvement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        var visqueux_compenent = GetComponent<JoueurVisqueux>();
       
 
       

     
        
    }

    // Update is called once per frame
       private void Update()
        {

               if (Input.GetKey(KeyCode.UpArrow)){
                Debug.Log("up!fake");
                foreach(var os in GetComponent<JoueurVisqueux>().GetSortedBones()){
                    Debug.Log("up");
                    GetComponent<JoueurVisqueux>().UnStick(1);
                    os.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(os.GetComponent<Rigidbody2D>().linearVelocity.x, Mathf.Min(os.GetComponent<Rigidbody2D>().linearVelocity.y + 1, 1));
                }
               }
                if (Input.GetKey(KeyCode.DownArrow)){
                 foreach(var os in GetComponent<JoueurVisqueux>().GetSortedBones()){
                      os.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(os.GetComponent<Rigidbody2D>().linearVelocity.x, Mathf.Max(os.GetComponent<Rigidbody2D>().linearVelocity.y - 1, -1));
                 }
                }
                if (Input.GetKey(KeyCode.LeftArrow)){
                 foreach(var os in GetComponent<JoueurVisqueux>().GetSortedBones()){
                      os.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Mathf.Max(os.GetComponent<Rigidbody2D>().linearVelocity.x - 1, -1), os.GetComponent<Rigidbody2D>().linearVelocity.y);
                 }
                }
                if (Input.GetKey(KeyCode.RightArrow)){
                 foreach(var os in GetComponent<JoueurVisqueux>().GetSortedBones()){
                      os.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Mathf.Min(os.GetComponent<Rigidbody2D>().linearVelocity.x + 1, 1), os.GetComponent<Rigidbody2D>().linearVelocity.y);
                 }
                }
                
        }
}
