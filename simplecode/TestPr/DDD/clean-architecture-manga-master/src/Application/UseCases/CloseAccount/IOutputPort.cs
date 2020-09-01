// <copyright file="IOutputPort.cs" company="Ivan Paulovich">
// Copyright © Ivan Paulovich. All rights reserved.
// </copyright>

namespace Application.UseCases.CloseAccount
{
    using Domain.Accounts;
    using Services;

    /// <summary>
    ///     Output Port.
    /// </summary>
    public interface IOutputPort
    {
        /// <summary>
        ///     Invalid input.
        /// </summary>
        void Invalid(Notification notification);

        /// <summary>
        ///     Account closed successfully.
        /// </summary>
        void Ok(Account account);

        /// <summary>
        ///     Account not found.
        /// </summary>
        void NotFound();

        /// <summary>
        ///     Account has funds.
        /// </summary>
        void HasFunds();
    }
}
