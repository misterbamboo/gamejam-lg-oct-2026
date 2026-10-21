using Assets.CustomerQueueDomain.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CustomerQueueDomain.Scripts
{
    public class CustomerQueue : MonoBehaviour
    {
        public event Action<Customer> CustomerAdded;
        public event Action<Customer> CustomerRemoved;

        public int Size => Customers.Count;

        public IReadOnlyList<Customer> All => Customers;

        [SerializeField] private int MaxCustomers = 6;

        [SerializeField] private float SpawnInterval = 10f;

        private List<Customer> Customers { get; set; } = new List<Customer>();

        // Starts at 0 so the first customer arrives immediately.
        private float timeUntilNextCustomer;

        private void Update()
        {
            // While full, hold the timer so the next customer arrives a full interval after a spot frees up.
            if (Size >= MaxCustomers)
            {
                timeUntilNextCustomer = SpawnInterval;
                return;
            }

            timeUntilNextCustomer -= Time.deltaTime;
            if (timeUntilNextCustomer <= 0f)
            {
                AddRandomCustomer();
                timeUntilNextCustomer += SpawnInterval;
            }
        }

        public void AddRandomCustomer()
        {
            var customer = Customer.Regular();
            Customers.Add(customer);
            CustomerAdded?.Invoke(customer);
        }

        public void Remove(Customer customer)
        {
            if (Customers.Remove(customer))
            {
                CustomerRemoved?.Invoke(customer);
            }
        }
    }
}
