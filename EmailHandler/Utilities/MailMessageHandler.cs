using EmailHandler.Models;

namespace EmailHandler.Utilities
{
    public static class MailMessageHandler
    {
        public static string GetHtmlTemplateForUserregistration(Event Event)
        {
            string myString = @"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
            text-align: center;
            color: #333333;
        }

        .container {
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        h1 {
            color: #004080;
            margin-bottom: 20px;
        }

        p {
            color: #666666;
            margin-bottom: 10px;
        }

        .banner {
            width: 100%;
            max-height: 150px;
            object-fit: cover;
            border-radius: 10px 10px 0 0;
        }

        .content {
            padding: 20px;
        }

        .button {
            display: inline-block;
            padding: 10px 20px;
            font-size: 16px;
            text-decoration: none;
            background-color: #004080;
            color: white;
            border-radius: 5px;
        }
    </style>
</head>
<body>
    <img src=""https://swag.gatech.edu/sites/default/files/2020-08/tower-facebook.jpg"" alt=""Banner Image"" class=""banner"">
    <div class=""container"">
        <div class=""content"">
            <h1>Welcome to Our Community!</h1>
            <p>Dear {{UserName}},</p>
            <p>Thank you for joining our community. We're thrilled to have you as a new member!</p>
            <p>Your account has been successfully registered.</p>
            <p>We look forward to sharing exciting updates and Events with you. Feel free to explore our platform.</p>
            <a href=""{{RedirectURL}}"" class=""button"">Explore Now</a>
        </div>
    </div>
</body>
</html>

";

            myString = myString.Replace("{{UserName}}", Event.UserName);
            myString = myString.Replace("{{RedirectURL}}", Event.RedirectUrl);

            return myString;
        }

        public static string GetHtmlTemplateForEventRegistration(Event Event)
        {

            var result = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
            text-align: center;
            color: #333333;
        }

        .container {
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        h1 {
            color: #004080;
            margin-bottom: 20px;
        }

        p {
            color: #666666;
            margin-bottom: 10px;
        }

        ul {
            list-style: none;
            padding: 0;
            margin: 0;
        }

        li {
            margin-bottom: 10px;
        }

        .banner {
            width: 100%;
            max-height: 150px;
            object-fit: cover;
            border-radius: 10px 10px 0 0;
        }

        .content {
            padding: 20px;
        }

        .button {
            display: inline-block;
            padding: 10px 20px;
            font-size: 16px;
            text-decoration: none;
            background-color: #004080;
            color: white;
            border-radius: 5px;
        }
    </style>
</head>
<body>
    <img src=""https://swag.gatech.edu/sites/default/files/2020-08/tower-facebook.jpg"" alt=""Banner Image"" class=""banner"">
    <div class=""container"">
        <div class=""content"">
            <h1>Event Registration Confirmation</h1>
            <p>Dear {{User}},</p>
            <p>Thank you for registering for our Event. We are excited to have you on board!</p>
            <p>Event Details:</p>
            <ul>
                <li><strong>Event Name:</strong> {{EventName}}</li>
                <li><strong>Date:</strong> {{EventDate}}</li>
                <li><strong>Location:</strong> {{EventLocation}}</li>
            </ul>
            <p>We look forward to seeing you there!</p>
            <a href=""{{RedirectURL}}"" class=""button"">Visit Our App</a>
        </div>
    </div>
</body>
</html>
";

            result = result.Replace("{{User}}", Event.UserName);
            result = result.Replace("{{RedirectURL}}", Event.RedirectUrl);
            result = result.Replace("{{EventName}}", Event.EventName);
            result = result.Replace("{{EventDate}}", Event.EventDate.ToString());
            result = result.Replace("{{EventLocation}}", Event.EventLocation);

            return result;
        }

        public static string GetHtmlTemplateForEventRegistrationCancellation(Event Event)
        {
            var result = @"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
            text-align: center;
            color: #333333;
        }

        .container {
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        h1 {
            color: #d9534f;
            margin-bottom: 20px;
        }

        p {
            color: #666666;
            margin-bottom: 10px;
        }

        .banner {
            width: 100%;
            max-height: 150px;
            object-fit: cover;
            border-radius: 10px 10px 0 0;
        }

        .content {
            padding: 20px;
        }

        .button {
            display: inline-block;
            padding: 10px 20px;
            font-size: 16px;
            text-decoration: none;
            background-color: #d9534f;
            color: white;
            border-radius: 5px;
        }
    </style>
</head>
<body>
    <img src=""https://swag.gatech.edu/sites/default/files/2020-08/tower-facebook.jpg"" alt=""Banner Image"" class=""banner"">
    <div class=""container"">
        <div class=""content"">
            <h1>Event Attendance Canceled</h1>
            <p>Dear {{User}},</p>
            <p>We regret to inform you that your attendance to the following Event has been canceled:</p>
            <p><strong>Event Name:</strong> {{EventName}}</p>
            <p><strong>Date:</strong> {{EventDate}}</p>
            <p><strong>Location:</strong> {{EventLocation}}</p>
            <p>If you have any questions or concerns, please feel free to reach out to us.</p>
            <p>We hope to see you at future Events!</p>
            <a href=""{{RedirectURL}}"" class=""button"">Explore More Events</a>
        </div>
    </div>
</body>
</html>

";

            result = result.Replace("{{User}}", Event.UserName);
            result = result.Replace("{{RedirectURL}}", Event.RedirectUrl);
            result = result.Replace("{{EventName}}", Event.EventName);
            result = result.Replace("{{EventDate}}", Event.EventDate.ToString());
            result = result.Replace("{{EventLocation}}", Event.EventLocation);

            return result;
        }

        public static string GetHtmlTemplateForEventEnrollment(Event Event)
        {
            var result = @"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
            text-align: center;
            color: #333333;
        }

        .container {
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        h1 {
            color: #004080;
            margin-bottom: 20px;
        }

        p {
            color: #666666;
            margin-bottom: 10px;
        }

        .banner {
            width: 100%;
            max-height: 150px;
            object-fit: cover;
            border-radius: 10px 10px 0 0;
        }

        .content {
            padding: 20px;
        }

        .button {
            display: inline-block;
            padding: 10px 20px;
            font-size: 16px;
            text-decoration: none;
            background-color: #004080;
            color: white;
            border-radius: 5px;
        }

        ul {
            list-style: none;
            padding: 0;
            margin: 0;
        }

        li {
            margin-bottom: 10px;
        }
    </style>
</head>
<body>
    <img src=""https://swag.gatech.edu/sites/default/files/2020-08/tower-facebook.jpg"" alt=""Banner Image"" class=""banner"">
    <div class=""container"">
        <div class=""content"">
            <h1>New Event Created</h1>
            <p>Dear {{User}},</p>
            <p>Congratulations! You've successfully created a new Event in our system.</p>
            <p>Event Details:</p>
            <p><strong>Event Name:</strong> {{EventName}}</p>
            <p><strong>Date:</strong> {{EventDate}}</p>
            <p><strong>Location:</strong> {{EventLocation}}</p>
            <p>Your Event has been registered, and we look forward to its success!</p>
            <a href=""{{RedirectURL}}"" class=""button"">View Event</a>
        </div>
    </div>
</body>
</html>

";

            result = result.Replace("{{User}}", Event.UserName);
            result = result.Replace("{{RedirectURL}}", Event.RedirectUrl);
            result = result.Replace("{{EventName}}", Event.EventName);
            result = result.Replace("{{EventDate}}", Event.EventDate.ToString());
            result = result.Replace("{{EventLocation}}", Event.EventLocation);

            return result;
        }
    }
}
