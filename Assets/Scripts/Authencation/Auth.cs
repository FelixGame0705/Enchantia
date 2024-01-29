using Firebase.Auth;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Auth : MonoBehaviour
{
    [SerializeField] TMP_Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            textBox.text += "Authenticate";
            //Auth with Google Play
            PlayGamesPlatform.Instance.Authenticate(status =>
            {
                if (status == SignInStatus.Success)
                {
                    textBox.text += "\nSuccess\nRequestServerSideAccess";

                    try
                    {
                        //Ask for Auth code - This is why we need the Web Server Client ID's and secrets from Firebase
                        PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                        {
                            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
                            Credential credential = PlayGamesAuthProvider.GetCredential(code);

                            //move Firebase get to Coroutine so we can log to our textbox
                            StartCoroutine(AuthGet());

                            IEnumerator AuthGet()
                            {
                                System.Threading.Tasks.Task<FirebaseUser> task = auth.SignInWithCredentialAsync(credential);

                                while (!task.IsCompleted) yield return null;

                                if (task.IsCanceled)
                                {
                                    textBox.text += "\ncanceled";
                                }
                                else if (task.IsFaulted)
                                {
                                    textBox.text += "\nerror " + task.Exception;
                                }
                                else
                                {
                                    FirebaseUser newUser = task.Result;
                                    textBox.text += "\n\nFULLY AUTHENTICATED\n\n";
                                    textBox.text += newUser.ToString();
                                }
                            }
                        });
                    }
                    catch (Exception e)
                    {
                        textBox.text += "RequestServerSideAccess error\n" + e.ToString();
                    }
                }
                else textBox.text += "Failure";
            });
        }
        catch (Exception e)
        {
            textBox.text += "Authenticate error\n" + e.ToString();
        }
    }
}