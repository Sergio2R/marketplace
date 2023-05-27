// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Marketplace.Core.Model;
using Microsoft.Data.Sqlite;

namespace Marketplace.Dal
{
    internal class MarketplaceDb : IMarketplaceDb, IDisposable
    {
        private readonly SqliteConnection _connection;

        public MarketplaceDb()
        {
            var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @".."));
            _connection = new SqliteConnection($@"Data Source={path}\Marketplace.Dal\marketplace.db");
            _connection.Open();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public async Task<User[]> GetUsersAsync()
        {
            // Retrieves a list of users along with the count of their associated offers

            await using var command = new SqliteCommand(
                "SELECT U.Id, U.Username, COUNT(O.Id) AS Offers\r\n" +
                "FROM User U LEFT JOIN Offer O ON U.Id = O.UserId\r\n" +
                "GROUP BY U.Id, U.Username;",
                _connection);

            try
            {
                await using var reader = await command.ExecuteReaderAsync();

                var results = new List<User>();

                while (await reader.ReadAsync())
                {
                    var user = new User
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Username = reader.GetString(reader.GetOrdinal("Username"))
                    };

                    results.Add(user);
                }

                return results.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Category[]> GetCategoriesAsync()
        {
            // Retrieves a list of categories from the database

            await using var command = new SqliteCommand(
                "SELECT Id, Name " +
                "FROM Category",
                _connection);

            try
            {
                await using var reader = await command.ExecuteReaderAsync();

                var results = new List<Category>();

                while (await reader.ReadAsync())
                {
                    var category = new Category
                    {
                        Id = reader.GetByte(reader.GetOrdinal("Id")),
                        Name = reader.GetString(reader.GetOrdinal("Name"))
                    };

                    results.Add(category);
                }

                return results.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Offer[]> GetOffersAsync()
        {
            // Retrieves a list of offers from the database

            await using var command = new SqliteCommand(
                "SELECT Id, CategoryId, Description, Location, PictureUrl, PublishedOn, Title, UserId FROM Offer ORDER BY Id DESC LIMIT 20",
                _connection);

            try
            {
                await using var reader = await command.ExecuteReaderAsync();

                var results = new List<Offer>();

                while (await reader.ReadAsync())
                {
                    var offer = new Offer
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        CategoryId = reader.GetByte(reader.GetOrdinal("CategoryId")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        Location = reader.GetString(reader.GetOrdinal("Location")),
                        PictureUrl = reader.GetString(reader.GetOrdinal("PictureUrl")),
                        PublishedOn = reader.GetDateTime(reader.GetOrdinal("PublishedOn")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        UserId = reader.GetInt32(reader.GetOrdinal("UserId"))
                    };

                    results.Add(offer);
                }

                return results.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public async Task<Offer[]> GetPaginatedOffersAsync(int pageIndex, int pageSize)
        {
            // Retrieves a paginated list of offers along with associated user and category details

            int offset = pageIndex * pageSize;

            await using var command = new SqliteCommand(
                $@"SELECT o.Id, o.CategoryId, o.Description, o.Location, o.PictureUrl, o.PublishedOn, o.Title, o.UserId, u.Username, c.Name AS CategoryName
           FROM Offer AS o
           JOIN ""User"" AS u ON o.UserId = u.Id
           JOIN Category AS c ON o.CategoryId = c.Id
           ORDER BY o.PublishedOn DESC
           LIMIT {pageSize} OFFSET {offset}",
                _connection);

            try
            {
                await using var reader = await command.ExecuteReaderAsync();

                var results = new List<Offer>();

                while (await reader.ReadAsync())
                {
                    var offer = new Offer
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        CategoryId = reader.GetByte(reader.GetOrdinal("CategoryId")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        Location = reader.GetString(reader.GetOrdinal("Location")),
                        PictureUrl = reader.GetString(reader.GetOrdinal("PictureUrl")),
                        PublishedOn = reader.GetDateTime(reader.GetOrdinal("PublishedOn")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                        UserName = reader.GetString(reader.GetOrdinal("Username")), // Retrieve the user name
                        CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")) // Retrieve the category name
                    };

                    results.Add(offer);
                }

                return results.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int?> GetUserIdByUsername(string username)
        {
            // Retrieves the user ID based on the given username

            await using var command = new SqliteCommand(
                "SELECT Id FROM User WHERE Username = @username LIMIT 1",
                _connection);
            command.Parameters.AddWithValue("@username", username);

            try
            {
                var result = await command.ExecuteScalarAsync();
                return result != DBNull.Value ? Convert.ToInt32(result) : (int?)null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Guid> CreateOfferAsync(Offer offer)
        {
            // Creates a new offer in the database

            var offerId = Guid.NewGuid();

            await using var command = new SqliteCommand(
                "INSERT INTO Offer (Id, CategoryId, Description, Location, PictureUrl, PublishedOn, Title, UserId) " +
                "VALUES (@id, @categoryId, @description, @location, @pictureUrl, @publishedOn, @title, @userId)",
                _connection);

            command.Parameters.AddWithValue("@id", offerId);
            command.Parameters.AddWithValue("@categoryId", offer.CategoryId);
            command.Parameters.AddWithValue("@description", offer.Description);
            command.Parameters.AddWithValue("@location", offer.Location);
            command.Parameters.AddWithValue("@pictureUrl", offer.PictureUrl);
            command.Parameters.AddWithValue("@publishedOn", offer.PublishedOn);
            command.Parameters.AddWithValue("@title", offer.Title);
            command.Parameters.AddWithValue("@userId", offer.UserId);

            try
            {
                await command.ExecuteNonQueryAsync();
                return offerId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> GetOffersCountAsync()
        {
            // Retrieves the count of offers in the database

            await using var command = new SqliteCommand(
                "SELECT COUNT(*) FROM Offer",
                _connection);

            try
            {
                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
