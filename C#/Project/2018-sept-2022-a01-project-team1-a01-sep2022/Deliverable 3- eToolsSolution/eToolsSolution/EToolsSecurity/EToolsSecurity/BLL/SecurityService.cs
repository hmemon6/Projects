// ***********************************************************************
// Assembly         : EToolsSecurity
// Author           : James Thompson
// Created          : 12-03-2022
//
// Last Modified By : James Thompson
// Last Modified On : 12-03-2022
// ***********************************************************************
// <copyright file="SecurityService.cs" company="EToolsSecurity">
//     Copyright (c) NAIT. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
#nullable disable
using EToolsSecurity.DAL;
using EToolsSecurity.ViewModel;

namespace EToolsSecurity.BLL
{
    /// <summary>
    /// Class SecurityService.
    /// </summary>
    public class SecurityService
    {
        #region Constructor and Context Dependency

        //  rename the context to match your system
        /// <summary>
        /// The context
        /// </summary>
        private readonly eTools2021Context _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        internal SecurityService(eTools2021Context context)
        {
            _context = context;
        }
        #endregion

        #region Services


        //  You will need to set up your extended method/backend dependencies

        //  Query to obtain the employee data
        /// <summary>
        /// Gets the employee information.
        /// </summary>
        /// <param name="isManager">if set to <c>true</c> [is manager].</param>
        /// <returns>EmployeeInfo.</returns>
        public EmployeeInfo GetEmployeeInfo(bool isManager)
        {
            int employeeId = isManager ? 3 : 1;
            return _context.Employees
                .Where(x => x.EmployeeID == employeeId)
                .Select(x => new EmployeeInfo()
                {
                    EmployeeID = x.EmployeeID,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    IsManager = isManager
                })
                .SingleOrDefault();
        }

        public EmployeeInfo GetEmployeeInfoDeptHead(bool isDeptHead)
        {
            int employeeId = isDeptHead ? 13 : 12;
            return _context.Employees
                .Where(x => x.EmployeeID == employeeId)
                .Select(x => new EmployeeInfo()
                {
                    EmployeeID = x.EmployeeID,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    IsManager = isDeptHead
                })
                .SingleOrDefault();
        }

        #endregion
    }
}