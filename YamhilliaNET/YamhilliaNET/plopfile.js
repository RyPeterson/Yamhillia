const fs = require('fs');
const path = require('path');
const migrationFolder = path.join(__dirname, 'Migrations');

function nextVersion() {
    return fs.readdirSync(migrationFolder)
        .filter(f => !f.includes('Extensions'))
        .length + 1;
}

module.exports = (plop) => {
    plop.setGenerator('migration', {
        description: 'Create a new migration',
        prompts: [{
            type: 'input',
            name: 'name',
            message: 'Migration name?'
        }],
        actions: [{
            type: 'add',
            path: 'Migrations/{{name}}Migration.cs',
            templateFile: 'plop-templates/migration.hbs',
            data: () => {
                return {
                    version: nextVersion()
                };
            }
        }]
    })
}