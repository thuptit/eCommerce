import { Fragment } from "react";
import { publicRouters } from "../src/routers";
import "./App.css";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";

function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
          {publicRouters.map((router, index) => {
            const Layout = router.layout || Fragment;
            const Page = router.component;
            return (
              <Route
                key={index}
                path={router.path}
                element={
                  <Layout>
                    <Page />
                  </Layout>
                }
              />
            );
          })}
        </Routes>
      </div>
    </Router>
  );
}

export default App;
