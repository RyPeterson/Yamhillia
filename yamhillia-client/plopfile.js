module.exports = function (plop) {
  // create your generators here
  plop.setGenerator("component", {
    description: "Create a component",
    prompts: [
      {
        type: "input",
        name: "name",
        message: "Component name",
      },
    ],
    actions: [
      {
        type: "add",
        path: "components/{{properCase name}}.tsx",
        templateFile: "plop-templates/component.hbs",
      },
    ],
  });

  plop.setGenerator("page", {
    description: "Create a page",
    prompts: [
      {
        type: "input",
        name: "name",
        message: "Page name",
      },
    ],
    actions: [
      {
        type: "add",
        path: "pages/{{name}}.tsx",
        templateFile: "plop-templates/page.hbs",
      },
    ],
  });
};
