import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

/* Components */
import Footer from './components/layout/Footer';
import Navbar from './components/layout/Navbar';
import Container from './components/layout/Container';
import Message from './components/layout/Message';

/* Pages */
import Login from './components/pages/Auth/Login';
import Register from './components/pages/Auth/Register';
import Home from './components/pages/Home';
import Profile from './components/pages/User/Profile';
import MyPets from './components/pages/Pet/MyPets';
import AddPet from './components/pages/Pet/AddPet';
import EditPet from './components/pages/Pet/EditPet';
import PetDetails from './components/pages/Pet/PetDetails';
import MyAdoptions from './components/pages/Pet/MyAdoptions';

/* Context */
import { UserProvider } from './context/UserContext';

function App() {
  return (
    <Router>
      <UserProvider>
        <Navbar />
        <Message />
        <Container>
          <Routes>
            <Route path='/login' element={<Login />} />
            <Route path='/register' element={<Register />} />
            <Route path='/user/profile' element={<Profile />} />
            <Route path='/pet/mypets' element={<MyPets />} />
            <Route path='/pet/add' element={<AddPet />} />
            <Route path='/pet/edit/:id' element={<EditPet />} />
            <Route path='/pet/:id' element={<PetDetails />} />
            <Route path='/pet/myadoptions' element={<MyAdoptions />} />
            <Route path='/' element={<Home />} />
          </Routes>
        </Container>
        <Footer />
      </UserProvider>
    </Router>
  );
}

/*
<Redirect from="/allusers" to="/users" />
\/
<Route path="/allusers" element={<Navigate to="/users" />}
*/

export default App;
