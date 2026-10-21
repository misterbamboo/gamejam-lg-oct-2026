using Assets.CustomerQueueDomain.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CustomerQueueDomain.Scripts
{
    public class CustomerQueue : MonoBehaviour
    {
        public int Size => Customers.Count;

        public IEnumerable<Customer> All => Customers;

        [SerializeField] private int QueueSize = 10;

        private Queue<Customer> Customers { get; set; } = new Queue<Customer>();

        private void Start()
        {
            for (int i = 0; i < QueueSize; i++)
            {
                var cust = Customer.Regular();
                Customers.Enqueue(cust);

                Debug.Log(cust.Preference.DisplayText());
            }
        }

        private void Update()
        {

        }
    }
}