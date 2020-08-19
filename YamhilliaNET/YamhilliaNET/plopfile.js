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
    });
    
    plop.setGenerator('model', {
        description: 'Create a new model',
        prompts: [{
            type: 'input',
            name: 'name',
            message: 'Model name?'
        }],
        actions: [{
            type: 'add',
            path: 'Models/Entities/{{name}}.cs',
            templateFile: 'plop-templates/model.hbs'
        }]
    })
}