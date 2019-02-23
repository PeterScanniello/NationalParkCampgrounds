-- Delete all of the data
DELETE FROM reservation;
DELETE FROM site;
DELETE FROM campground
DELETE FROM park;

-- Insert a fake park
INSERT INTO park VALUES ('New Park', 'Iowa', '2019-02-23', 34, 1, 'This park is great.');
DECLARE @newPark int = (SELECT @@IDENTITY);

-- Insert a fake campground
INSERT INTO campground VALUES (@newPark, 'New Campground', 2, 11, 30);
DECLARE @newCampground int = (SELECT @@IDENTITY);

-- Insert a fake site
INSERT INTO site VALUES (@newCampground, 1, 10, 1, 20, 0);
DECLARE @newSite int = (SELECT @@IDENTITY);

-- Create a fake reservation
INSERT INTO reservation VALUES (@newSite, 'Bob Reservation', '2019-05-12', '2019-05-13', GETDATE());
DECLARE @newReservation int = (SELECT @@IDENTITY);

-- Return the id of the fake city
SELECT @newReservation as newReservationId,
@newPark as newParkId,
@newCampground as newCampgroundId;