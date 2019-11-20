# Odyssey of the Mind Registration

## Overview

This repository contains the code for the Judge and Tournament Registration for NoVA North Odyssey of the Mind.

## Technologies

It is currently built using ASP.NET MVC version 4.

## Configuration

### Files to Configure

Make sure to copy the web.config file from **this directory** into the root directory of your website.

1. Note: This should be placed in a higher directory than your bin, Content, Views, etc. directories.  The directory containing those subdirectories will likely have its own web.config file.

### Hosting Company Configuration

1. Make sure that the ASP.NET MVC website directory is set as an application starting point.
    1. Log into the [Winhost Control Panel](https://cp.winhost.com).
    2. Navigate to the Odyssey website.
    3. Navigate to the Sites List -> Site Manager -> Application Starting Point page.
    4. Make sure that the path to the directory containing the website is registered as an application starting point, e.g. /registration.

## SQL Server Database

1. Make sure to back up the database after every season (or before the next one).