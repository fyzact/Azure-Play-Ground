module.exports = async function (context, req,inputDocument) {
    context.log('JavaScript HTTP trigger function processed a request.');

    if(!inputDocument){
        let message="Todo item"+req.Query.id+"not found";
        context.log(message);
        context.req={
            // status:404,
            body:message
        }
    }
  
    context.res = {
        // status: 200, /* Defaults to 200 */
        body: inputDocument.desc
    };
}