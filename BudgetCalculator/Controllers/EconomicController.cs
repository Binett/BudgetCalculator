using BudgetCalculator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BudgetCalculator.Controllers
{
    /// <summary>
    /// The controller of EconomicObject objects.
    /// </summary>
    public class EconomicController
    {
        private List<EconomicObject> EconomicObjectList;
        private List<string> errorLog;

        #region Public Methods
        /// <summary>
        /// Constructor for EconomicController 
        /// instanciate a new list of EconomicObject
        /// </summary>
        public EconomicController()
        {
            EconomicObjectList = new List<EconomicObject>();
            errorLog = new List<string>();
        }

        public List<EconomicObject> GetList => EconomicObjectList;

        /// <summary>
        /// Add Economic object to EconomicObjectList
        /// </summary>
        /// <param name="name">string name</param>
        /// <param name="type">Type of object</param>
        /// <param name="amount">double amount</param>
        /// <returns>bool true or false</returns>
        public bool AddEconomicObjectToList(string name, EconomicType type, double amount)
        {
            if (IsValidString(name) && IsAmountMoreThanZero(amount) && !DoListContainName(name))
            {
                EconomicObjectList.Add(new EconomicObject
                {
                    Name = name,
                    Type = type,
                    Amount = amount,
                });
                return true;
            }

            return false;
        }

        /// <summary>
        /// Remove an economic object from EconomicObjectList
        /// </summary>
        /// <param name="name">string name</param>
        /// <returns>bool true or false</returns>
        public bool RemoveEconomicObjectFromList(string name)
        {
            if (IsValidString(name))
            {
                if (DoListContainName(name))
                {
                    EconomicObjectList.RemoveAll(x => x.Name == name);
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Updates an economic objects amount 
        /// </summary>
        /// <param name="name">string name</param>
        /// <param name="newAmount">double new amount</param>
        /// <returns>bool true if success</returns>
        public bool UpdateEconomicObjectAmount(string name, double newAmount)
        {
            if (IsValidString(name) && IsAmountMoreThanZero(newAmount) && DoListContainName(name))
            {
                foreach (var ecoObj in EconomicObjectList)
                {
                    if (ecoObj.Name.Contains(name))
                    {
                        ecoObj.Amount = newAmount;
                        return true;
                    }
                }
            }

            return false;  
        }


        /// <summary>
        /// Updates an economic object name if name
        /// dosent exist
        /// </summary>
        /// <param name="oldName">string old name</param>
        /// <param name="newName">string new name</param>
        /// <returns>bool true if success else false</returns>
        public bool UpdateEconomicObjectName(string oldName, string newName)
        {
            if(IsValidString(oldName) && IsValidString(newName))
            {
                foreach(var ecoObj in EconomicObjectList)
                {
                    if(ecoObj.Name.Contains(oldName))
                    {
                        ecoObj.Name = newName;
                        return true;
                    }
                }

                return false;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Checks so the string name dosent contain
        /// null, string empty and string whitespace
        /// </summary>
        /// <param name="check">string check</param>
        /// <returns>bool if success</returns>
        private bool IsValidString(string check)
        {
            if(!IsStringNull(check))
            {
                if(!IsStringEmpty(check))
                {
                    if(!IsStringPreWhitespace(check))
                    {
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("String name had a whitespace as first character");
                        //impelement logger logic
                        return false;
                    }
                }
                else
                {
                    Debug.WriteLine("String name was empty");
                    //impelement logger logic
                    return false;
                }
            }
            else
            {
                Debug.WriteLine("String name was null");
                //impelement logger logic
                return false;
            }
        }

       
        /// <summary>
        /// Checks if string is null
        /// </summary>
        /// <param name="check">string check</param>
        /// <returns>if string is null return false</returns>
        private bool IsStringNull(string check)
        {
            return check == null;
        }

        /// <summary>
        /// Checks if string is Empty
        /// </summary>
        /// <param name="check">string check</param>
        /// <returns>if string is empty return false</returns>
        private bool IsStringEmpty(string check)
        {
            return check == "";
        }

        /// <summary>
        ///  Checks if string is starts with whitespace
        /// </summary>
        /// <param name="check">string check</param>
        /// <returns>if string starts with whitespace return false</returns>
        private bool IsStringPreWhitespace(string check)
        {
            return check[0] == ' ';
        }

        /// <summary>
        /// Checks if objects amount is greater than 0
        /// </summary>
        /// <param name="amount">double amount</param>
        /// <returns>If double amount is greater than 0 return true</returns>
        private bool IsAmountMoreThanZero(double amount)
        {
            if(amount > 0)
            {
                return true;
            }
            else
            {
                Debug.WriteLine("Amount was less than zero");
                //Logic to save to logger
                return false;
            }
        }

        /// <summary>
        /// Checks if objects name already exists 
        /// </summary>
        /// <param name="name">string name</param>
        /// <returns>bool true if name dosent exists</returns>
        private bool DoListContainName(string name)
        {
            foreach (var ecoObj in EconomicObjectList)
            {
                if (ecoObj.Name.Contains(name))
                {
                    return true;
                }
            }

            Debug.WriteLine("String name does not exist in economic object list");
            //Logic to save to logge
            return false;
        }
        #endregion
    }
}