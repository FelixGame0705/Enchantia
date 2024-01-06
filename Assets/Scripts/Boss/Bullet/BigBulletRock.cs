using CarterGames.Assets.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBulletRock : BulletController
{
    public AnimationCurve curve;
    public GolemBoss golemBoss;
    public GameObject VfxExploision;
    public GameObject Model;

    [SerializeField] private float duration = 1.0f;

    [SerializeField] private float maxHeightY = 3.0f;

    private Transform target;

    private Vector3 start;
    private Animator animatorExploision;

    private IEnumerator Move()
    {
        yield return StartCoroutine(Curve(start, target.position));
        // impact
    }

    public void Init(Transform start, Transform Target)
    {
        target = Target;
        this.start = start.position;
        animatorExploision = VfxExploision.GetComponentInChildren<Animator>();
        StartCoroutine(Move());
    }

    public IEnumerator Curve(Vector3 start, Vector3 finish)
    {
        var timePast = 0f;


        //temp vars
        while (timePast < duration)
        {
            timePast += Time.deltaTime;

            var linearTime = timePast / duration; //0 to 1 time
            var heightTime = curve.Evaluate(linearTime); //value from curve

            var height = Mathf.Lerp(0f, maxHeightY, heightTime); //clamped between the max height and 0

            transform.position =
                Vector3.Lerp(start, finish, linearTime) + new Vector3(0f, height, 0f); //adding values on y axis

            yield return null;
        }
        VfxExploision.SetActive(true);
        Model.SetActive(false);
        yield return new WaitUntil(()=>IsAnimationFinished("Explosion"));
        golemBoss.bulletRockPoolInstance.GetComponent<ObjectPool>().ReturnObjectToPool(gameObject);
    }

    protected override void OnTriggerEnter2D(Collider2D collision) { }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Damage " + collision.gameObject.layer);
        if (collision.gameObject.layer == 7)
        {
            AudioManager.instance.Play("Hit", GameData.Instance.GetVolumeAudioGame());
            //SpawnExploision();
            collision.gameObject.GetComponent<CharacterController>().TakeDamage(BulletDataConfig.BulletConfig.Damage);
            //golemBoss.bulletRockPoolInstance.GetComponent<ObjectPool>().ReturnObjectToPool(gameObject);
            DynamicTextManager.CreateText2D(collision.transform.position, BulletDataConfig.BulletConfig.Damage.ToString(), DynamicTextManager.defaultData);
        }
    }

    private bool IsAnimationFinished(string animationName)
    {
        // Ki?m tra xem animation có ?ang ch?y không
        if (animatorExploision.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            // Ki?m tra xem animation ?ã hoàn t?t ch?a
            if (!animatorExploision.IsInTransition(0) && animatorExploision.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                return true; // Animation ?ã hoàn t?t
            }
        }

        return false; // Animation ?ang ch?y ho?c không t?n t?i
    }
}
