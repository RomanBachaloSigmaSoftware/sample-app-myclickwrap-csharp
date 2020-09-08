export const LoadClickwrapApi = (baseUrl, callback) => {
  const existingScript = document.getElementById("clickWrapScript");
  const scriptUrl = "/clickapi/sdk/latest/docusign-click.js";

  if (!existingScript) {
    const script = document.createElement("script");
    script.src = baseUrl + scriptUrl;
    script.id = "clickWrapScript";
    document.body.prepend(script);

    script.onload = () => {
      if (callback) callback();
    };
  }

  if (existingScript && callback) callback();
};
