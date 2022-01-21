public class CustumerTestController
{
    /// <summary>
    /// Test GetCustumer() method
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async void GetCustumer()
    {
        #region Arrange
        var options = new DbContextOptionsBuilder<ModelContext>()
            .UseInMemoryDatabase(databaseName: "custumerdb")
            .Options;
 
        using (var ctx = new ModelContext(options))
        {
            await ctx.Custumer.AddAsync(new Custumer
            {
                CustumerId = "61eac1cb602fd5a75e11b5de",
                Name = "Alessio",
                Surname = "Verdi",
                Dateofbirth = "01/01/1950",
                Email = "alessioverdi@microservice.it"
            });
            await ctx.SaveChangesAsync();
        }
        Custumer existingCustumer = null;
        Custumer non_existingCustumer = null;
        #endregion
 
        #region Act
        using (var ctx = new ModelContext(options))
        {
            var controller = new CustumerController(ctx);
            existingCustumer = await controller.GetCustumer(1);
            non_existingCustumer = await controller.GetCustumer(2);
 
        }
        #endregion
 
        #region Assert
        Assert.True(existingCustumer != null && non_existingCustumer == null);
        #endregion
    }
}