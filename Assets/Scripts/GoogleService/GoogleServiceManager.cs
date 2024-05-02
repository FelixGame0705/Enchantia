using UnityEngine;
using Unity.Services.Core;
using System.Threading.Tasks;
using GooglePlayGames;
public class GoogleServiceManager : Singleton<GoogleServiceManager>
{
    [SerializeField] private LeaderBoardService _leaderboardService;
    [SerializeField] private PaymentService _paymentService;
    [SerializeField] private AuthencationService _authService;
    [SerializeField] private AdsService _adsService;
    [SerializeField] private PlayerService playerService;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        PlayGamesPlatform.Activate();
    }

    public delegate void DisableGoogleService();
    public DisableGoogleService DisableServiceUseGoogle{get; set;}

    public LeaderBoardService LeaderboardService {get {return _leaderboardService;}}
    public PaymentService PaymentService {get {return _paymentService;}}
    public AuthencationService AuthencationService {get { return _authService;}}
    public AdsService AdsService {get {return _adsService;}}
    public PlayerService PlayerService {get => playerService;}
}