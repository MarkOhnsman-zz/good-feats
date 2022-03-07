CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE IF NOT EXISTS restaurants(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) NOT NULL,
  address VARCHAR(255) NOT NULL,
  category VARCHAR(255) NOT NULL,
  picture VARCHAR(255) NOT NULL
) default charset utf8 COMMENT '';

-- DROP TABLE restaurants;
-- DROP TABLE reviews;

INSERT INTO restaurants(name, address, category, picture)
VALUES("Micks Mac", "Boise, ID", "American", "https://www.lactaid.com/sites/lactaid_us/files/recipe-images/mac-and-cheese-website.png");
INSERT INTO restaurants(name, address, category, picture)
VALUES("Harrys Hammz", "Boise, ID", "Ham", "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/christmas-ham-1633960990.jpg?crop=1.00xw:0.752xh;0,0.0625xh&resize=1200:*");
INSERT INTO restaurants(name, address, category, picture)
VALUES("Mocha Marks", "Boise, ID", "Coffee", "https://pbs.twimg.com/profile_images/1107812841992519680/46U3PAZ8_400x400.jpg");
INSERT INTO restaurants(name, address, category, picture)
VALUES("Mocha Micks", "Boise, ID", "Coffee", "https://pbs.twimg.com/profile_images/1107812841992519680/46U3PAZ8_400x400.jpg");

SELECT * FROM restaurants;

CREATE TABLE IF NOT EXISTS reviews(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  body VARCHAR(255) NOT NULL,
  rating int NOT NULL,
  restaurantId int NOT NULL, 
  creatorId VARCHAR(255) NOT NULL,
  published TINYINT DEFAULT 0,

  FOREIGN KEY(restaurantId) REFERENCES restaurants(id) ON DELETE CASCADE,

  FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE
  ) default charset utf8 COMMENT '';

  SELECT * FROM reviews;

  INSERT INTO reviews(body, rating, restaurantId, creatorId, published)
  VALUES("Awesome! Didnt need a lactase pill. Super neat", 5, 1, "61ddc49c03d1a5bca8e4210a", 1);
  INSERT INTO reviews(body, rating, restaurantId, creatorId, published)
  VALUES("Kind of a weird dude running the place but decent mocha i guess", 3, 3, "61ddc49c03d1a5bca8e4210a", 1);
  INSERT INTO reviews(body, rating, restaurantId, creatorId, published)
  VALUES("didnt have any chocolate oat milk. Sad", 2, 3, "61ddc49c03d1a5bca8e4210a", 1);
  INSERT INTO reviews(body, rating, restaurantId, creatorId, published)
  VALUES("literally the only thing they have is ham. 5/5", 5, 2, "61ddc49c03d1a5bca8e4210a", 1);
  INSERT INTO reviews(body, rating, restaurantId, creatorId, published)
  VALUES("pretty sure I saw john cena eating here", 4, 2, "61ddc49c03d1a5bca8e4210a", 0);
  

  SELECT
      rs.*,
      AVG(rv.rating) AS AverageRating,
      COUNT(rv.id) AS TotalReviews
      FROM restaurants rs
      LEFT JOIN reviews rv on rv.restaurantId = rs.id AND rv.published = 1
      GROUP BY rs.id;
  SELECT
      rs.*,
      AVG(rv.rating) AS AverageRating,
      COUNT(rv.id) AS TotalReviews
      FROM restaurants rs
      LEFT JOIN reviews rv on rv.restaurantId = rs.id AND rv.published = 1
      WHERE rs.id = 9;

