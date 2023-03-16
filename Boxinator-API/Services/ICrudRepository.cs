namespace Boxinator_API.Services
{
    public interface ICrudRepository<T, ID>
    {
        /// <summary>
        /// Post a object of T to the database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<T> Create(T obj, ID userId);

        /// <summary>
        /// Get all rows from table
        /// </summary>
        /// <returns>List of T</returns>
        Task<IEnumerable<T>> ReadAll(ID userId);

        /// <summary>
        /// Get row from table with id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception">Thrown if T is not found</exception>
        /// <returns>Object of T</returns>
        Task<T> ReadById(ID id, ID userId);

        /// <summary>
        /// Sends a Put 
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="Exception">Thrown if T is not found</exception>
        /// <returns></returns>
        Task<T> Update(T obj, ID userId);

        /// <summary>
        /// Deletes data with id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception">Thrown if T is not found</exception>
        /// <returns></returns>
        Task Delete(ID id,ID userId);
    }
}
