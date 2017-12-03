using LD40.UI.Menus;
using LD40.VFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD40
{
    public class Controller : ManagedObject
    {
        public Camera playerCamera;

        public float cooldown;
        private float cd;
        public Projectile proj;
        public Rigidbody Shell;
        public bool Godmode;

        public Transform SpawnLocation;

        public bool Exploded = false;

        public Transform muzzle;
        public Flasher flasher;

        public Transform top;
        public Transform bottom;
        public Transform exploded;

        public Vector3 velocity;
        public float maxSpeed;
        public float acceleration;
        public float brake;

        public void Update()
        {
            if(cd > 0)
            {
                cd -= Time.deltaTime * Time.timeScale;
                if (cd < 0)
                    cd = 0;
            }

            if (!Exploded && Game.LevelCompleted == false)
            {
                CharacterMovement();

                Vector3 screenXY = playerCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, (playerCamera.transform.position.y - transform.position.y)));
                Vector3 topXY = playerCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, (playerCamera.transform.position.y - top.position.y)));

                PointArmTowardsPointAlways(topXY);

                Debug.DrawRay(screenXY, Vector3.down * 100, Color.red, 1f);

                if (Input.GetMouseButtonDown(0) && Game.ShotsFired < Game.ShotsMax && cd == 0)
                {
                    Projectile p = Instantiate(proj) as Projectile;
                    p.transform.position = muzzle.transform.position;
                    p.Launch(muzzle.forward, muzzle.transform.position);
                    p.Tank = this;
                    Game.Projectiles.Add(p.gameObject);

                    Rigidbody r = Instantiate(Shell) as Rigidbody;
                    r.transform.position = new Vector3(top.transform.position.x, top.transform.position.y + 0.5f, top.transform.position.z);
                    r.AddForce(new Vector3(Random.Range(-500, 500), 500, Random.Range(-500, 500)));
                    r.angularVelocity = new Vector3(Random.Range(-500, 500), 5, Random.Range(-500, 500));

                    Game.ShotsFired++;
                    Game.GetShotCounter().UpdateShots();
                    cd = cooldown;

                    SendAll(OnShoot);
                    if(Game.ShotsFired == 1)
                        ThrowEvent(Event.OnFirstShot);
                    ThrowEvent(Event.OnShoot);
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    Game.GetShotCounter().GetComponentInChildren<TextColorFlash>().FlashTextColor(Color.red, 0.25f);
                }
            }
        }

        public void StartOnDeath()
        {
            SendAll(OnDeath);
            ThrowEvent(Event.OnDeath);
        }

        public void OnTriggerEnter(Collider col)
        {
            if(col.tag == "goal")
            {
                if (Game.LevelCompleted == false)
                {
                    //win
                    Debug.Log("WIN");
                    StartCoroutine(Fanfare());
                    Game.LevelCompleted = true;
                    flasher.FlashWin();
                    Time.timeScale = 0.25f;
                }
            }
        }

        public IEnumerator Fanfare()
        {
            Godmode = true;

            EndScreenMenu menu = Game.GetMenuManager().Pop<EndScreenMenu>(); //this is why this is nice to do this <--

            //yield return new WaitForEndOfFrame();

            menu.Title.text = Game.LevelTitle;
            menu.ShotText.text = "You fired " + Game.ShotsFired + (Game.ShotsFired == 1 ? " shot." : " shots");
            menu.TimeText.text = "It took you " + Game.Timer.ToString("###,##0.00") + " seconds.";
            menu.LetterGrade.text = LetterGradeCalculator.GetGradeFromScore(Game.Timer, Game.ALevelTime, Game.ShotsFired, Game.ALevelShots).ToString();

            yield return new WaitForSecondsRealtime(4);
            //display some text maybe
            //SceneManager.LoadScene("Tutorial2", LoadSceneMode.Single);
        }

        public void DestroyProjectiles()
        {
            transform.position = new Vector3(9999, 999, 9999); //for colliders

            for(int i = 0; i < Game.Projectiles.Count; i++)
            {
                Destroy(Game.Projectiles[i]);
            }

            Game.Projectiles.Clear();
        }

        public void StartReset()
        {
            top.gameObject.SetActive(true);
            bottom.gameObject.SetActive(true);
            exploded.gameObject.SetActive(false);
            Exploded = false;
            Game.FreezeTrails = false;
            Game.GetMiddleText().ClearQueue();
            velocity = Vector3.zero;
            transform.position = SpawnLocation.position;

            Game.ShotsFired = 0;
            Game.GetShotCounter().UpdateShots();
            Game.Timer = 0;
            Game.LevelCompleted = false;
            Time.timeScale = 1;
            Game.GetMiddleText().Hide();
            Godmode = false;

            GameObject[] debris = GameObject.FindGameObjectsWithTag("debris");

            for (var i = 0; i < debris.Length; i++)
            {
                Destroy(debris[i]);
            }

            SendAll(Reset);
        }

        private void CharacterMovement()
        {
            int forward = 0;

            if (Input.GetKey(KeyCode.W)) forward = 1;
            if (Input.GetKey(KeyCode.S)) forward = -1;
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) forward = 0;

            if (forward > 0)
            {
                velocity = new Vector3(velocity.x, velocity.y, velocity.z + (acceleration * Time.deltaTime));
            }
            else if (forward < 0)
            {
                velocity = new Vector3(velocity.x, velocity.y, velocity.z + (-acceleration * Time.deltaTime));
            }
            else
            {
                if (velocity.z > 0)
                {
                    if (velocity.z < brake * Time.deltaTime)
                        velocity = new Vector3(velocity.x, velocity.y, 0);
                    else
                        velocity = new Vector3(velocity.x, velocity.y, velocity.z -= brake * Time.deltaTime);
                }
                else
                {
                    if (velocity.z > -brake * Time.deltaTime)
                        velocity = new Vector3(velocity.x, velocity.y, 0);
                    else
                        velocity = new Vector3(velocity.x, velocity.y, velocity.z += brake * Time.deltaTime);
                }

                if (Mathf.Abs(velocity.z) < 0.1) velocity = new Vector3(velocity.x, velocity.y, 0);
            }

            int strafe = 0;

            if (Input.GetKey(KeyCode.D)) strafe = 1;
            if (Input.GetKey(KeyCode.A)) strafe = -1;
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)) strafe = 0;

            if (strafe > 0)
            {
                velocity = new Vector3(velocity.x + (acceleration * Time.deltaTime), velocity.y, velocity.z);
            }
            else if (strafe < 0)
            {
                velocity = new Vector3(velocity.x + (-acceleration * Time.deltaTime), velocity.y, velocity.z);
            }
            else
            {
                if (velocity.x > 0)
                {
                    if (velocity.x < brake * Time.deltaTime)
                        velocity = new Vector3(0, velocity.y, velocity.z);
                    else
                        velocity = new Vector3(velocity.x -= brake * Time.deltaTime, velocity.y, velocity.z);
                }
                else
                {
                    if (velocity.x > -brake * Time.deltaTime)
                        velocity = new Vector3(0, velocity.y, velocity.z);
                    else
                        velocity = new Vector3(velocity.x += brake * Time.deltaTime, velocity.y, velocity.z);
                }

                if (Mathf.Abs(velocity.x) < 0.1) velocity = new Vector3(0, velocity.y, velocity.z);
            }

            Vector3 clamped = Vector3.ClampMagnitude(new Vector3(velocity.x, 0, velocity.z), maxSpeed);
            velocity = new Vector3(clamped.x, velocity.y, clamped.z);

            GetComponent<CharacterController>().SimpleMove(velocity);
        }

        public void PointArmTowardsPointAlways(Vector3 pos)
        {
            top.LookAt(pos, Vector3.up);
        }
    }
}
