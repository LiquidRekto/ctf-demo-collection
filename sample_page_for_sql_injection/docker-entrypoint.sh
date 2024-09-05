#!/bin/bash
# Run MySQL setup commands
mysql -u root -p"$MYSQL_ROOT_PASSWORD" -e "CREATE DATABASE IF NOT EXISTS mydatabase;"

# Run Flask app
flask run --host=0.0.0.0