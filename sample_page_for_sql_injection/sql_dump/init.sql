DROP TABLE IF EXISTS userinfo;

CREATE TABLE userinfo (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(255) NOT NULL,
    `password` VARCHAR(255) NOT NULL UNIQUE
);

INSERT INTO userinfo (username, `password`) VALUES
('admin', '123');