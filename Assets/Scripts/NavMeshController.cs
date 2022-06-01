using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent))]

//public class NavMeshController : MonoBehaviour
//{
//    [SerializeField] private float goalDistance = 10f;
//    [SerializeField] private Transform[] destinationGoals;

//    NavMeshAgent navMeshAgent;

//    private int currentGoalIndex = 0;

//    private void Awake()
//    {
//        navMeshAgent = GetComponent<NavMeshAgent>();
//        navMeshAgent.destination = destinationGoals[currentGoalIndex].position;
//    }

//    void Update()
//    {
//        if (navMeshAgent.remainingDistance <= goalDistance)
//        {
//            currentGoalIndex = ++currentGoalIndex;

//            if (currentGoalIndex == destinationGoals.Length)
//            {
//                currentGoalIndex = 0;
//            }

//            navMeshAgent.destination = destinationGoals[currentGoalIndex].position;
//        }
//    }
//}
