var Username = 'admin@gmail.com';
var Password = 'admin@gmail.com';

pm.sendRequest({
    url: "https://localhost:44367/api/userapi/token",
    method: 'POST',
    header: {
        'Content-Type': 'application/json',
    },
    body: {
        mode: 'raw',
        raw: JSON.stringify({
            UserName: Username,
            Password: Password
        }),
        options: {
            raw: {
                language: 'json'
            }
        }
    }
}, function (err, res) {

    console.log(res.json().token);
    pm.globals.set("token", res.json().token);
})
