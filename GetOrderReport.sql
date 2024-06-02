CREATE PROCEDURE GetOrderReport
    @employeeId INT = NULL,
    @clientId INT = NULL,
    @productId INT = NULL
AS
BEGIN
    SELECT Employees.EmployeeName, Clients.ClientName, Orders.OrderDate, Products.ProductName, Items.Quantity, 
           (ItemPrice * Quantity) AS TotalPrice
    FROM Items
    INNER JOIN Orders ON Orders.OrderID = Items.Order_OrderID
    INNER JOIN Products ON Products.ProductID = Items.Product_ProductID
    INNER JOIN Employees ON Orders.Employee_EmployeeID = Employees.EmployeeID
    INNER JOIN Clients ON Orders.Client_ClientID = Clients.ClientID
    WHERE (@employeeId IS NULL OR Orders.Employee_EmployeeID = @employeeId)
      AND (@clientId IS NULL OR Orders.Client_ClientID = @clientId)
      AND (@productId IS NULL OR Items.Product_ProductID = @productId)
END