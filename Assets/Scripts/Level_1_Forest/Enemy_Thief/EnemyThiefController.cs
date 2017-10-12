using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Главный управляющий класс игрового объекта типа EnemyThief. Отвечает за все действия игрового объекта
///</summary
public class EnemyThiefController : MonoBehaviour {
    private Transform thePlayer;
    public float moveSpeed;
    private bool isMoveRight;

    public float playerRange;

    private Transform _tr;
    private Animator _ator;
    private Rigidbody2D _r2d;


    private bool isInRange;
    public AudioClip playerHurt;

    public float returnTime;
    private bool isReturning;
    private float timeCheck;

    private bool isFacingAway;
    public bool isFollowOnLookAway;


    ///<summary>
    /// Проверка на стенку
    ///</summary
    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool isWallHitted;

    private bool atEdge;
    public Transform edgeCheck;

    public float yDifference;

    private bool isAttacking;
  //public float attackTime;
    private float attackCheck;
    public int hurtPlayerValue;

    private bool playerWasHit;

    public float angryTime;
    private float angryCheck;
    private bool inAngryState;
  
    private ThiefAttackChecker thiefAttackChecker;

    // Use this for initialization
    void Start() {
        ///<summary>
        /// Кеширование нужных компонентов вора
        ///</summary
        _tr = GetComponent<Transform>();
        _ator = GetComponent<Animator>();
        _r2d = GetComponent<Rigidbody2D>();

        thePlayer = FindObjectOfType<PlayerController>().GetComponent<Transform>();

        ///<summary>
        /// Получение скрипта ThiefAttackChecker для работы с playerWasHit
        ///</summary
        thiefAttackChecker = (ThiefAttackChecker)GetComponentInChildren(typeof(ThiefAttackChecker));

        PlayerWasHit = false;
        isAttacking = false;
        inAngryState = false;
      //attackCheck = attackTime;
    }

    void Update() {
        ///<summary>
        /// Если галочка isFollowOnLookAway не поставлена- то апдейт ничего не делает
        ///</summary
        if (!isFollowOnLookAway) return;

        ///<summary>
        /// inAngryState проверка на агро - когда юнит стоит и ничего не делает некоторое время
        /// стоит выше атаки из-за приоритета (inAngryState = true - даже при isAttacking = true
        /// юнит не будет атаковать
        ///</summary
        if (inAngryState) {  
            angryCheck -= Time.deltaTime;
            
            if (angryCheck < 0) {
                DeAngryMode();
            }
            return;
        }

        ///<summary>
        /// if(!_ator.GetBool("isAttacking")) isAttacking = false; для того, чтобы не было случаев,когда 
        /// анимация уже прервана другим действием,но isAttacking = true чем мешает дальнейшему
        ///</summary
        if (isAttacking) {
            if(!_ator.GetBool("isAttacking")) isAttacking = false;
            return;
        }

        ///<summary>
        /// Проверки для скорости(включение анимации передвижения) и удара об стенку или края
        ///</summary
        _ator.SetFloat("Speed", Mathf.Abs(_r2d.velocity.x));
        isWallHitted = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
        atEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

        ///<summary>
        /// Для определения, смотрит ли игрок в сторону юнита,или от него
        ///</summary
        if ((thePlayer.position.x < _tr.position.x && thePlayer.localScale.x < 0) ||
            (thePlayer.position.x > _tr.position.x && thePlayer.localScale.x > 0)) {
            isFacingAway = true;
        }
        else {
            isFacingAway = false;
        }

        ///<summary>
        /// isReturning = true - юнит идет в другую сторону от игрока время returnTime, 
        /// работает если юнит дошел до обрыва.
        /// Если юнит во время возращения дошел до другого обрыва и timeCheck > 0 то он
        /// туда упадет
        ///</summary
        if (isReturning) {
            timeCheck -= Time.deltaTime;
            if (timeCheck < 0) {
                isReturning = false;
                CheckDirection();
                if (IfInRangeX()) MovingDirection();
                return;
            }
            MovingDirection();
            return;
        }

        ///<summary>
        /// Проверка на нахождение игрока в зоне видимости юнита
        ///</summary
        if (!(IfInRangeX() && IfInRangeY())) return;

        ///<summary>
        /// Если игрок смотрит в другую сторону от игрока,то юнит начинает идти к нему
        ///</summary
        if (isFacingAway) {
            if (isWallHitted || !atEdge) {
                ToStepBefore();
                MovingDirection();
                timeCheck = Time.deltaTime + returnTime;
                isReturning = true;
                return;
            }
            CheckDirection();
            MovingDirection();
        }
    }

    ///<summary>
    /// Методы проверок на нахождение игрока в зоне видимости юнита по оси X и Y
    ///</summary
    private bool IfInRangeX() {
        if (Mathf.Abs(thePlayer.position.x - _tr.position.x) < playerRange) {
            isInRange = true;
            return true;
        }
        isInRange = false;
        return false;
    }
    private bool IfInRangeY() {
        if ((Mathf.Abs(thePlayer.position.y - _tr.position.y) < yDifference)) {
            return true;
        }
        return false;
    }

    ///<summary>
    /// Метод для изменения направления движения в случае возвращения юнита
    ///</summary
    void ToStepBefore() {

        isMoveRight = !isMoveRight;

    }

    ///<summary>
    /// Метод проверки на направление движения юнита в сторону игрока
    ///</summary
    void CheckDirection() {
        if (thePlayer.position.x > _tr.position.x) {
            isMoveRight = true;
        }
        else if (thePlayer.position.x < _tr.position.x) {
            isMoveRight = false;
        }
    }
    ///<summary>
    /// Метод движения юнита 
    ///</summary
    void MovingDirection() {
        if (isMoveRight) {
            _tr.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            _r2d.velocity = new Vector2(moveSpeed, _r2d.velocity.y);
        }
        else {
            _tr.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            _r2d.velocity = new Vector2(-moveSpeed, _r2d.velocity.y);
        }
    }

    ///<summary>
    /// Метод уничтожения игрового объекта, используется после завершения анимации Death
    ///</summary
    public void Destroyer() {
        Destroy(gameObject);
    }

    ///<summary>
    /// Метод используется для активации анимации для атаки
    ///</summary
    public void ActivateTrigger() {
        if (isAttacking) return;
        _ator.SetBool("isAttacking", true);
      //  attackCheck = attackTime;
        isAttacking = true;
        _r2d.velocity = new Vector2(0, _r2d.velocity.y);
    }

    ///<summary>
    /// Метод используется для деактивации анимации для атаки
    ///</summary
    public void DeActivateTrigger() {
        _ator.SetBool("isAttacking", false);
        isAttacking = false;
    }

    ///<summary>
    /// Метод используется для деактивации анимации ярости
    ///</summary
    public void DeAngryMode() {
        _ator.SetBool("isAngry", false);
        inAngryState = false;
        PlayerWasHit = false;
    }
    ///<summary>
    /// Метод используется для активации анимации ярости
    ///</summary
    public void CheckAngryMode() {
        if ((!PlayerWasHit) && (!inAngryState)) {
            _ator.SetBool("isAngry", true);
            angryCheck = angryTime;
            inAngryState = true;
        }
    }

    ///<summary>
    /// Метод используется для активации звука удара в анимации
    ///</summary
    public void PlaySoundIfWasHit() {
        if (PlayerWasHit) {
            VoiceManager.me.PlayNoiseSound(playerHurt); 
        }
    }
    ///<summary>
    /// Метод используется для получения урона в анимации
    ///</summary
    public void HitPlayer() {
        if (PlayerWasHit) HealthManager.HurtPlayer(hurtPlayerValue);
    }
    ///<summary>
    /// Метод используется для проверки получения удара игроком
    ///</summary
    public void CheckIfPlayerWasHit() {

        PlayerWasHit = thiefAttackChecker.PlayerWasHit;
    }

    public bool IsAttacking {
        get {
            return isAttacking;
        }
        set {
            isAttacking = value;
        }
    }
   

    public bool PlayerWasHit {
        get {
            return playerWasHit;
        }

        set {
            playerWasHit = value;
        }
    }
}
