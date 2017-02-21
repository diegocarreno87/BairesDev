namespace BDev.BLL
{
    using System;
    using BDev.Common;
    using Data;
    public class UserBLL : IBLL<User>
    {
        private UserRepository repository;

        public UserBLL()
        {
            repository = new UserRepository();
        }

        public bool ValidateUserPassword(User user)
        {
            return this.repository.ValidateUserPassword(user);
        }

        /// <summary>
        /// TODO: Create a new login user
        /// </summary>
        /// <param name="e"></param>
        public void Create(User e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Delete an existing login user
        /// </summary>
        /// <param name="iD"></param>
        public void Delete(int iD)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Update an existing login user
        /// </summary>
        /// <param name="newE"></param>
        public void Edit(User newE)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Get the info for an existing login user
        /// </summary>
        /// <param name="iD"></param>
        /// <returns></returns>
        public User GetByID(int iD)
        {
            throw new NotImplementedException();
        }
    }
}
