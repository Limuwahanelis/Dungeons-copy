using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    public Type aa;
    [Header("Debug"), SerializeField] bool _printState;
    public bool IsAlive => _isAlive;
    public PlayerState CurrentPlayerState => _currentPlayerState;
    public GameObject MainBody => _mainBody;
    [Header("Player"), SerializeField] Animator _anim;
    [SerializeField] GameObject _mainBody;
    [SerializeField] AnimationManager _playerAnimationManager;
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] PlayerChecks _playerChecks;
    [SerializeField] PlayerCombat _playerCombat;
    //[SerializeField] PlayerCollisions _playerCollisions;
    [SerializeField] PlayerHealthSystem _playerHealthSystem;
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] AudioEvent _event;
    [SerializeField] AudioEventPlayer _player;
    private PlayerState _currentPlayerState;
    private PlayerContext _context;
    private Dictionary<Type, PlayerState> playerStates = new Dictionary<Type, PlayerState>();
    private bool _isAlive = true;
    [SerializeField,HideInInspector] private string _initialStateType;
    void Start()
    {

        Initalize();
    }
    protected void Initalize()
    {
        //_playerHealthSystem.OnPushed += PushPlayer;
        List<Type> states = AppDomain.CurrentDomain.GetAssemblies().SelectMany(domainAssembly => domainAssembly.GetTypes())
            .Where(type => typeof(PlayerState).IsAssignableFrom(type) && !type.IsAbstract).ToArray().ToList();

        _context = new PlayerContext
        {
            ChangePlayerState = ChangeState,
            animationManager = _playerAnimationManager,
            playerMovement = _playerMovement,
            WaitAndPerformFunction = WaitAndExecuteFunction,
            WaitFrameAndPerformFunction = WaitFrameAndExecuteFunction,
            coroutineHolder = this,
            checks = _playerChecks,
            combat = _playerCombat,
            //collisions = _playerCollisions,
        };

        PlayerState.GetState getState = GetState;
        foreach (Type state in states)
        {
            playerStates.Add(state, (PlayerState)Activator.CreateInstance(state, getState));
        }
        // Set Startitng state
        Logger.Log(Type.GetType(_initialStateType));
         PlayerState newState = GetState(Type.GetType(_initialStateType));
         newState.SetUpState(_context);
         _currentPlayerState = newState;
        Logger.Log(newState.GetType());
    }
    public PlayerState GetState(Type state)
    {
        return playerStates[state];
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.K)) _player.PlayeAudioEvent(_event);
        _currentPlayerState.Update();
    }
    private void FixedUpdate()
    {
        _currentPlayerState.FixedUpdate();
    }
    public void ChangeState(PlayerState newState)
    {
        if (_printState) Logger.Log(newState.GetType());
        _currentPlayerState.InterruptState();
        _currentPlayerState = newState;
    }

    public void PushPlayer(PushInfo psuhInfo)
    {
       // _playerMovement.PushPlayer(psuhInfo);
        _currentPlayerState.Push();
    }
    public Coroutine WaitAndExecuteFunction(float timeToWait, Action function)
    {
        return StartCoroutine(HelperClass.DelayedFunction(timeToWait, function));
    }
    public Coroutine WaitFrameAndExecuteFunction(Action function)
    {
        return StartCoroutine(HelperClass.WaitFrame(function));
    }

    private void OnDestroy()
    {
       // _playerHealthSystem.OnPushed -= PushPlayer;
    }
}
